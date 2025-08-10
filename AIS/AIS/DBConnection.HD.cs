// Routes methods that call pkg_hd.* stored procedures.
using AIS.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;

namespace AIS.Controllers
{
    public partial class DBConnection
    {
        public string GetUserName(string PPNUMBER)
        {
            string userName = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "pkg_hd.p_ppno_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = PPNUMBER;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userName = rdr["EMPLOYEE_NAME"].ToString();
                }
            }
            con.Dispose();
            return userName;
        }

        public List<BranchModel> GetZoneBranches(int zone_code = 0, bool sessionCheck = true)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<BranchModel> branchList = new List<BranchModel>();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "pkg_hd.P_GetOldParasEntityid";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = zone_code;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    BranchModel br = new BranchModel();
                    br.BRANCHID = Convert.ToInt32(rdr["branchentityid"]);
                    br.BRANCHNAME = rdr["branchname"].ToString();
                    branchList.Add(br);
                }
            }
            con.Dispose();
            return branchList;
        }
    }
}
