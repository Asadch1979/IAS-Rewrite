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
        public List<GlHeadDetailsModel> GetGlheadDetails(int engId = 0, int gl_code = 0)
        {
            int ENG_ID = this.GetLoggedInUserEngId();
            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GlHeadDetailsModel> list = new List<GlHeadDetailsModel>();
            using (OracleCommand cmd = CreateSanitizedCommand(con))
            {
                cmd.CommandText = "pkg_ai.p_getglheadsummary";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = engId;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GlHeadDetailsModel GlHeadDetails = new GlHeadDetailsModel();
                    GlHeadDetails.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);
                    GlHeadDetails.GL_TYPEID = Convert.ToInt32(rdr["GL_TYPEID"]);
                    GlHeadDetails.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    GlHeadDetails.BALANCE = Convert.ToDouble(rdr["BALANCE"]);
                    if (rdr["DEBIT"].ToString() != null && rdr["DEBIT"].ToString() != "")
                        GlHeadDetails.DEBIT = Convert.ToDouble(rdr["DEBIT"]);
                    if (rdr["CREDIT"].ToString() != null && rdr["CREDIT"].ToString() != "")
                        GlHeadDetails.CREDIT = Convert.ToDouble(rdr["CREDIT"]);
                    list.Add(GlHeadDetails);
                }
            }
            con.Dispose();
            return list;
         }
    }
}
