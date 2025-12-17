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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
        private string CAU_KEY => _configuration?["Security:CAUKey"] ?? string.Empty;

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

        private SanitizedOracleCommand CreateSanitizedCommand(OracleConnection connection)
        {
            return new SanitizedOracleCommand(connection, SanitizeVarcharParameters);
        }

        private void SanitizeVarcharParameters(OracleCommand command)
        {
            foreach (OracleParameter parameter in command.Parameters)
            {
                if (parameter.OracleDbType != OracleDbType.Varchar2 || parameter.Value == null)
                    continue;

                if (parameter.Value is not string textValue)
                    continue;

                parameter.Value = IsRichTextParameter(parameter.ParameterName)
                    ? SanitizeRichText(textValue)
                    : SanitizePlainText(textValue);
            }
        }

        private static bool IsRichTextParameter(string? parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                return false;

            string[] richTextKeys = new[] { "OBSERVATION", "RESPONSE", "ANNEXURE", "BODY", "COMMENT", "PARAGRAPH" };
            return richTextKeys.Any(key => parameterName.Contains(key, StringComparison.OrdinalIgnoreCase));
        }

        private static string SanitizePlainText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            string withoutHtml = Regex.Replace(value, "<[^>]+>", string.Empty, RegexOptions.Multiline);
            string trimmed = Regex.Replace(withoutHtml, "[\\r\\n]+", " ").Trim();
            return trimmed.TrimStart('=', '+', '-', '@');
        }

        private static string SanitizeRichText(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            string withoutScripts = Regex.Replace(value, "<script[^>]*?>.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return withoutScripts;
        }

        private string DecryptPassword(string encryptedPassword)
        {
            byte[] bytes = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(bytes);
        }

        #region Session Handling
        public static string getMd5Hash(string input)
        {
            using var md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
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
            using (OracleCommand cmd = CreateSanitizedCommand(con))
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
                using (OracleCommand cmd = CreateSanitizedCommand(con))
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
            using (OracleCommand cmd = CreateSanitizedCommand(con))
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
                using (OracleCommand cmd = CreateSanitizedCommand(con))
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
            byte[] data = RandomNumberGenerator.GetBytes(length);
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[data[i] % validChars.Length];
            }
            return new string(password);
        }
        #endregion

        #region Cross-package
        public List<object> GetObservationText(int OBS_ID, int RESP_ID)
        {
            // NOTE: duplicate removed during partials normalization (see de-dup rules).
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            string ob_text = "";
            string ob_resp = "";

            List<object> list = new List<object>();

            using (OracleCommand cmd = CreateSanitizedCommand(con))
            {
                cmd.CommandText = "pkg_ae.P_GetObservationText";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("OBS_ID", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ob_text = rdr["TEXT"].ToString();
                }
                list.Add(ob_text);
                if (RESP_ID > 0)
                {
                    cmd.CommandText = "pkg_ar.P_GetOBSERVATIONSAUDITEERESPONSE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("OBS_ID", OracleDbType.Int32).Value = OBS_ID;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader rdr2 = cmd.ExecuteReader();

                    while (rdr2.Read())
                    {
                        ob_resp = rdr2["REPLY"].ToString();
                    }
                    list.Add(ob_resp);
                    List<AuditeeResponseEvidenceModel> modellist = new List<AuditeeResponseEvidenceModel>();
                    cmd.CommandText = "pkg_ar.P_get_AUDITEE_OBSERVATION_RESPONSE_evidences";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("RESP_ID", OracleDbType.Int32).Value = RESP_ID;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader rdr3 = cmd.ExecuteReader();
                    while (rdr3.Read())
                    {
                        AuditeeResponseEvidenceModel am = new AuditeeResponseEvidenceModel();
                        am.FILE_ID = rdr3["ID"].ToString();
                        am.IMAGE_NAME = rdr3["FILE_NAME"].ToString();
                        am.IMAGE_DATA = "";
                        am.SEQUENCE = Convert.ToInt32(rdr3["SEQUENCE"].ToString());
                        am.IMAGE_TYPE = rdr3["FILE_TYPE"].ToString();
                        modellist.Add(am);
                    }
                    list.Add(modellist);

                }
                else
                {
                    list.Add("");
                    list.Add(new List<object>());
                }
            }
            con.Dispose();
            return list;
        }
        #endregion

        private class SanitizedOracleCommand : OracleCommand
        {
            private readonly Action<OracleCommand> _sanitize;

            public SanitizedOracleCommand(OracleConnection connection, Action<OracleCommand> sanitize)
            {
                Connection = connection;
                _sanitize = sanitize;
            }

            public override OracleDataReader ExecuteReader()
            {
                _sanitize(this);
                return base.ExecuteReader();
            }

            public override OracleDataReader ExecuteReader(CommandBehavior behavior)
            {
                _sanitize(this);
                return base.ExecuteReader(behavior);
            }

            public override int ExecuteNonQuery()
            {
                _sanitize(this);
                return base.ExecuteNonQuery();
            }

            public override object ExecuteScalar()
            {
                _sanitize(this);
                return base.ExecuteScalar();
            }
        }
    }
}
