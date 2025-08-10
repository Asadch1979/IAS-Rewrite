using AIS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AIS.Controllers
{
    public partial class DBConnection : Controller
    {
        private SessionHandler sessionHandler;
        private readonly SQLParams sqlParams = new SQLParams();
        private readonly LocalIPAddress iPAddress = new LocalIPAddress();
        private readonly DateTimeHandler dtime = new DateTimeHandler();
        private readonly CAUEncodeDecode encoderDecoder = new CAUEncodeDecode();
        public ISession _session;
        public IHttpContextAccessor _httpCon;
        public IConfiguration _configuration;
        private readonly string CAU_KEY = "112233";

        [Obsolete]
        private readonly IHostingEnvironment _env;

        [Obsolete]
        public DBConnection(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env, IConfiguration configuration)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _httpCon = httpContextAccessor;
            _env = env;
            _configuration = configuration;
        }

        public DBConnection()
        {
        }

        #region Database Connection
        private OracleConnection DatabaseConnection()
        {
            try
            {
                OracleConnection con = new OracleConnection();
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                ocsb.Password = _configuration["ConnectionStrings:DBUserPassword"];
                ocsb.UserID = _configuration["ConnectionStrings:DBUserName"];
                ocsb.DataSource = _configuration["ConnectionStrings:DBDataSource"];
                ocsb.IncrPoolSize = 5;
                ocsb.MaxPoolSize = 5000;
                ocsb.MinPoolSize = 1;
                ocsb.Pooling = true;
                ocsb.ConnectionTimeout = 3540;
                con.ConnectionString = ocsb.ConnectionString;
                return con;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        private string DecryptPassword(string encryptedPassword)
        {
            byte[] bytes = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(bytes);
        }

        #region Session Handling
        public static string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool DisposeLoginSession()
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var sessionUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "pkg_lg.Session_END";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = sessionUser.PPNumber;
                cmd.Parameters.Add("SessionId", OracleDbType.Varchar2).Value = sessionUser.SessionId;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = sessionUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = sessionUser.UserRoleID;
                cmd.ExecuteReader();
            }
            con.Dispose();
            sessionHandler.DisposeUserSession();
            return true;
        }

        public bool IsLoginSessionExist(string PPNumber = "")
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var sessionUser = sessionHandler.GetSessionUser();

            if (PPNumber == "")
                PPNumber = sessionUser.PPNumber;

            bool isSession = false;
            if (!string.IsNullOrEmpty(PPNumber))
            {
                var con = this.DatabaseConnection();
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "pkg_lg.p_get_user_session";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = PPNumber;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (!string.IsNullOrEmpty(rdr["ID"].ToString()))
                            isSession = true;
                    }
                }
                con.Dispose();
            }
            return isSession;
        }

        public bool KillExistSession(LoginModel login)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var enc_pass = getMd5Hash(DecryptPassword(login.Password));
            var con = this.DatabaseConnection();
            con.Open();
            bool isSession = false;
            using (OracleCommand cmd = con.CreateCommand())
            {
                string _sql = "pkg_lg.p_get_user";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = login.PPNumber;
                cmd.Parameters.Add("enc_pass", OracleDbType.Varchar2).Value = enc_pass;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandText = _sql;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmd.CommandText = "pkg_lg.Session_Kill";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = login.PPNumber;
                    cmd.ExecuteReader();
                    isSession = true;
                }
            }
            con.Dispose();
            sessionHandler.DisposeUserSession();
            return isSession;
        }

        public bool TerminateIdleSession()
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            bool isTerminate = false;
            if (!string.IsNullOrEmpty(loggedInUser.PPNumber))
            {
                var con = this.DatabaseConnection();
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "pkg_lg.Session_Kill";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.ExecuteReader();
                    isTerminate = true;
                }
                con.Dispose();
                sessionHandler.DisposeUserSession();
            }
            return isTerminate;
        }

        // Utility methods
        public string GeneratePassword(int length = 8)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] password = new char[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                rng.GetBytes(data);
                for (int i = 0; i < length; i++)
                {
                    password[i] = validChars[data[i] % validChars.Length];
                }
            }
            return new string(password);
        }
        #endregion
        }
    }