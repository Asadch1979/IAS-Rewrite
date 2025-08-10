using AIS.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AIS.Controllers
    {
    public partial class DBConnection
        {
        public List<AuditPeriodModel> GetAuditPeriodStatus()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPeriodModel> periodList = new List<AuditPeriodModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GET_Auditperiod_status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditPeriodModel period = new AuditPeriodModel();
                    period.STATUS_ID = Convert.ToInt32(rdr["ID"]);
                    period.STATUS = rdr["STATUS"].ToString();
                    periodList.Add(period);
                    }
                }
            con.Dispose();
            return periodList;
            }

        public List<AuditCCQModel> GetCCQ(int ENTITY_ID = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditCCQModel> list = new List<AuditCCQModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetCCQ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditCCQModel chk = new AuditCCQModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        {
                        chk.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                        chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                        }
                    else
                        {
                        chk.ENTITY_NAME = "";
                        }

                    chk.QUESTIONS = rdr["QUESTIONS"].ToString();
                    if (rdr["CONTROL_VIOLATION_ID"].ToString() != null && rdr["CONTROL_VIOLATION_ID"].ToString() != "")
                        {
                        chk.CONTROL_VIOLATION_ID = Convert.ToInt32(rdr["CONTROL_VIOLATION_ID"]);
                        chk.CONTROL_VIOLATION = rdr["VIOLATION_NAME"].ToString();
                        }
                    else
                        {
                        chk.CONTROL_VIOLATION = "";
                        }
                    if (rdr["RISK_ID"].ToString() != null && rdr["RISK_ID"].ToString() != "")
                        {
                        chk.RISK_ID = Convert.ToInt32(rdr["RISK_ID"].ToString());
                        chk.RISK = rdr["RISK_DEF"].ToString();
                        }
                    else
                        {
                        chk.RISK = "";
                        }

                    chk.STATUS = rdr["STATUS"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<AuditPeriodModel> GetAuditPeriods(int dept_code = 0, int AUDIT_PERIOD_ID = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPeriodModel> periodList = new List<AuditPeriodModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditPeriods";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditPeriodModel period = new AuditPeriodModel();
                    period.AUDITPERIODID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    period.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    period.START_DATE = Convert.ToDateTime(rdr["START_DATE"]);
                    period.END_DATE = Convert.ToDateTime(rdr["END_DATE"]);
                    period.STATUS_ID = Convert.ToInt32(rdr["STATUS_ID"]);
                    period.STATUS = rdr["STATUS"].ToString();
                    periodList.Add(period);
                    }
                }
            con.Dispose();
            return periodList;
            }

        public string UpdateAuditPeriod(AuditPeriodModel periodModel)
            {
            string resp = "";
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_Update_AuditPeriod";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = periodModel.AUDITPERIODID;
                cmd.Parameters.Add("S_ID", OracleDbType.Int32).Value = periodModel.STATUS_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["REMARKS"].ToString();

                    }
                }
            con.Dispose();
            return resp;
            }

        public string AddAuditPeriod(AuditPeriodModel periodModel)
            {
            string resp = "";
            sessionHandler = new SessionHandler();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_AddAuditPeriod";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = periodModel.DESCRIPTION;
                cmd.Parameters.Add("STARTDATE", OracleDbType.Date).Value = periodModel.START_DATE;
                cmd.Parameters.Add("ENDDATE", OracleDbType.Date).Value = periodModel.END_DATE;

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["REMARKS"].ToString();

                    }
                }
            con.Dispose();
            return resp;
            }
        public List<AuditTeamModel> GetAuditTeams(int dept_code = 0, int userEntId = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditTeamModel> teamList = new List<AuditTeamModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditTeams";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("dept_code", OracleDbType.Int32).Value = dept_code;
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = userEntId != 0 ? userEntId : loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditTeamModel team = new AuditTeamModel();
                    team.ID = Convert.ToInt32(rdr["ID"]);
                    team.T_ID = Convert.ToInt32(rdr["T_ID"]);
                    team.CODE = rdr["T_CODE"].ToString();
                    team.NAME = rdr["TEAM_NAME"].ToString();
                    team.AUDIT_DEPARTMENT = Convert.ToInt32(rdr["PLACE_OF_POSTING"]);
                    team.TEAMMEMBER_ID = Convert.ToInt32(rdr["MEMBER_PPNO"]);
                    team.IS_TEAMLEAD = rdr["ISTEAMLEAD"].ToString();
                    team.PLACE_OF_POSTING = rdr["AUDIT_DEPARTMENT"].ToString();
                    team.EMPLOYEENAME = rdr["MEMBER_NAME"].ToString();
                    team.STATUS = rdr["STATUS"].ToString();
                    teamList.Add(team);
                    }
                }
            con.Dispose();
            return teamList;
            }
        public string AddAuditTeam(AuditTeamModel aTeam)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            string resp = "";
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_addauditteam";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("teamname", OracleDbType.Varchar2).Value = aTeam.NAME;
                cmd.Parameters.Add("TEAMMEMBER_ID", OracleDbType.Int32).Value = aTeam.TEAMMEMBER_ID;
                cmd.Parameters.Add("MAX_T_ID", OracleDbType.Int32).Value = aTeam.T_ID;
                cmd.Parameters.Add("EMPLOYEENAME", OracleDbType.Varchar2).Value = aTeam.EMPLOYEENAME;
                cmd.Parameters.Add("IS_TEAMLEAD", OracleDbType.Varchar2).Value = aTeam.IS_TEAMLEAD;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = aTeam.STATUS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["remarks"].ToString();
                    }

                }
            con.Dispose();
            return resp;
            }
        public bool DeleteAuditTeam(string T_CODE)
            {
            if (T_CODE != "" && T_CODE != null)
                {
                sessionHandler = new SessionHandler();
                sessionHandler._httpCon = this._httpCon;
                sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
                var con = this.DatabaseConnection(); con.Open();
                var loggedInUser = sessionHandler.GetSessionUser();
                using (OracleCommand cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "pkg_pg.P_DeleteAuditTeam";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("TID", OracleDbType.Int32).Value = T_CODE;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.ExecuteReader();

                    }
                con.Dispose();
                return true;
                }
            else
                return false;
            }
        public int GetLatestTeamID()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            int maxTeamId = 1;
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_MAXTEAMID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    if (rdr["MAX_T_ID"].ToString() != null && rdr["MAX_T_ID"].ToString() != "")
                        {
                        maxTeamId = Convert.ToInt32(rdr["MAX_T_ID"]);
                        }
                    }

                }
            con.Dispose();
            return maxTeamId;
            }
        public bool AddAuditCriteria(AddAuditCriteriaModel acm)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            bool isAlreadyAdded = true;

            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_ADDAUDITCRITERIA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENTITYTYPEID", OracleDbType.Int32).Value = acm.ENTITY_TYPEID;
                cmd.Parameters.Add("SIZEID", OracleDbType.Int32).Value = acm.SIZE_ID;
                cmd.Parameters.Add("RISKID", OracleDbType.Int32).Value = acm.RISK_ID;
                cmd.Parameters.Add("FREQUENCYID", OracleDbType.Int32).Value = acm.FREQUENCY_ID;
                cmd.Parameters.Add("NOOFDAYS", OracleDbType.Int32).Value = acm.NO_OF_DAYS;
                cmd.Parameters.Add("visit", OracleDbType.Varchar2).Value = acm.VISIT;
                cmd.Parameters.Add("APPROVALSTATUS", OracleDbType.Int32).Value = acm.APPROVAL_STATUS;
                cmd.Parameters.Add("AUDITPERIODID", OracleDbType.Int32).Value = acm.AUDITPERIODID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = acm.ENTITY_ID;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = "AUDIT CRITERIA CREATED";
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    if (rdr["REF"].ToString() != "" && rdr["REF"].ToString() != null && rdr["REF"].ToString() == "1")
                        {
                        isAlreadyAdded = false;
                        }

                    }
                }
            con.Dispose();
            return !isAlreadyAdded;
            }
        public bool UpdateAuditCriteria(AddAuditCriteriaModel acm, string COMMENTS)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_UpdateAuditCriteria";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = acm.ID;
                cmd.Parameters.Add("ENTITYTYPEID", OracleDbType.Int32).Value = acm.ENTITY_TYPEID;
                cmd.Parameters.Add("SIZEID", OracleDbType.Int32).Value = acm.SIZE_ID;
                cmd.Parameters.Add("RISKID", OracleDbType.Int32).Value = acm.RISK_ID;
                cmd.Parameters.Add("FREQUENCYID", OracleDbType.Int32).Value = acm.FREQUENCY_ID;
                cmd.Parameters.Add("NOOFDAYS", OracleDbType.Int32).Value = acm.NO_OF_DAYS;
                cmd.Parameters.Add("VISITS", OracleDbType.Varchar2).Value = acm.VISIT;
                cmd.Parameters.Add("AUDITPERIOD_ID", OracleDbType.Int32).Value = acm.AUDITPERIODID;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = COMMENTS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.ExecuteReader();
                }
            con.Dispose();
            return true;
            }
        public bool SetAuditCriteriaStatusReferredBack(int ID, string REMARKS)
            {
            if (REMARKS == "")
                REMARKS = "REFERRED BACK";
            sessionHandler = new SessionHandler
                {
                _httpCon = this._httpCon,
                _session = this._session,
                _configuration = this._configuration
                };
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_SetAuditCriteriaStatusReferredBack";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = ID;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.ExecuteReader();
                }
            con.Dispose();
            return true;
            }
        public string SetAuditCriteriaStatusApprove(int ID, string REMARKS)
            {
            if (REMARKS == "")
                REMARKS = "APPROVED";

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            string REMARK = "";
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_SetAuditCriteriaStatusApprove";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("CAID", OracleDbType.Int32).Value = ID;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    REMARK = rdr["remark"].ToString();

                    }


                }
            con.Dispose();
            return REMARK;
            }
        public List<AuditCriteriaModel> GetPendingAuditCriterias()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditCriteriaModel> criteriaList = new List<AuditCriteriaModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetPendingAuditCriterias";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditCriteriaModel acr = new AuditCriteriaModel();
                    acr.ID = Convert.ToInt32(rdr["ID"]);
                    acr.ENTITY_TYPEID = Convert.ToInt32(rdr["ENTITY_TYPEID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        acr.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    if (rdr["SIZE_ID"].ToString() != null && rdr["SIZE_ID"].ToString() != "")
                        acr.SIZE_ID = Convert.ToInt32(rdr["SIZE_ID"]);
                    acr.RISK_ID = Convert.ToInt32(rdr["RISK_ID"]);
                    acr.FREQUENCY_ID = Convert.ToInt32(rdr["FREQUENCY_ID"]);
                    acr.NO_OF_DAYS = Convert.ToInt32(rdr["NO_OF_DAYS"]);
                    acr.APPROVAL_STATUS = Convert.ToInt32(rdr["APPROVAL_STATUS"]);
                    acr.AUDITPERIODID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    acr.PERIOD = rdr["PERIOD"].ToString();
                    acr.ENTITY = rdr["ENTITY"].ToString();
                    acr.ENTITY_NAME = rdr["NAME"].ToString();
                    acr.FREQUENCY = rdr["FREQUENCY"].ToString();
                    acr.SIZE = rdr["BRSIZE"].ToString();
                    acr.RISK = rdr["RISK"].ToString();
                    acr.VISIT = rdr["VISIT"].ToString();
                    acr.COMMENTS = rdr["REMARKS"].ToString();
                    if (rdr["no_of_entity"].ToString() != null && rdr["no_of_entity"].ToString() != "")
                        acr.ENTITIES_COUNT = Convert.ToInt32(rdr["no_of_entity"].ToString());
                    criteriaList.Add(acr);
                    }
                }
            con.Dispose();
            return criteriaList;
            }
        public List<AuditCriteriaModel> GetRefferedBackAuditCriterias()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            var con = this.DatabaseConnection(); con.Open();
            List<AuditCriteriaModel> criteriaList = new List<AuditCriteriaModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetRefferedBackAuditCriterias";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditCriteriaModel acr = new AuditCriteriaModel();
                    acr.ID = Convert.ToInt32(rdr["ID"]);
                    acr.ENTITY_TYPEID = Convert.ToInt32(rdr["ENTITY_TYPEID"]);
                    if (rdr["SIZE_ID"].ToString() != null && rdr["SIZE_ID"].ToString() != "")
                        acr.SIZE_ID = Convert.ToInt32(rdr["SIZE_ID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        acr.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    acr.RISK_ID = Convert.ToInt32(rdr["RISK_ID"]);
                    acr.FREQUENCY_ID = Convert.ToInt32(rdr["FREQUENCY_ID"]);
                    acr.NO_OF_DAYS = Convert.ToInt32(rdr["NO_OF_DAYS"]);
                    acr.APPROVAL_STATUS = Convert.ToInt32(rdr["APPROVAL_STATUS"]);
                    acr.AUDITPERIODID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    acr.PERIOD = rdr["PERIOD"].ToString();
                    acr.ENTITY = rdr["ENTITY"].ToString();
                    acr.FREQUENCY = rdr["FREQUENCY"].ToString();
                    acr.SIZE = rdr["BRSIZE"].ToString();
                    acr.RISK = rdr["RISK"].ToString();
                    acr.ENTITY_NAME = rdr["NAME"].ToString();
                    acr.VISIT = rdr["VISIT"].ToString();
                    acr.COMMENTS = rdr["REMARKS"].ToString();// this.GetAuditCriteriaLogLastStatus(acr.ID);
                    criteriaList.Add(acr);
                    }
                }
            con.Dispose();
            return criteriaList;
            }
        public List<AuditCriteriaModel> GetAuditCriteriasToAuthorize()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditCriteriaModel> criteriaList = new List<AuditCriteriaModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditCriteriasToAuthorize";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditCriteriaModel acr = new AuditCriteriaModel();
                    acr.ID = Convert.ToInt32(rdr["ID"]);
                    acr.ENTITY_TYPEID = Convert.ToInt32(rdr["ENTITY_TYPEID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        acr.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    if (rdr["SIZE_ID"].ToString() != null && rdr["SIZE_ID"].ToString() != "")
                        acr.SIZE_ID = Convert.ToInt32(rdr["SIZE_ID"]);
                    acr.RISK_ID = Convert.ToInt32(rdr["RISK_ID"]);
                    acr.FREQUENCY_ID = Convert.ToInt32(rdr["FREQUENCY_ID"]);
                    acr.NO_OF_DAYS = Convert.ToInt32(rdr["NO_OF_DAYS"]);
                    acr.APPROVAL_STATUS = Convert.ToInt32(rdr["APPROVAL_STATUS"]);
                    acr.AUDITPERIODID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    acr.PERIOD = rdr["PERIOD"].ToString();
                    //acr.ENTITY = rdr["ENTITY"].ToString();
                    acr.FREQUENCY = rdr["FREQUENCY"].ToString();
                    acr.SIZE = rdr["BRSIZE"].ToString();
                    acr.RISK = rdr["RISK"].ToString();
                    acr.VISIT = rdr["VISIT"].ToString();
                    acr.ENTITY_NAME = rdr["NAME"].ToString();
                    acr.COMMENTS = rdr["REMARKS"].ToString();// this.GetAuditCriteriaLogLastStatus(acr.ID);
                    acr.ENTITIES_COUNT = this.GetExpectedCountOfAuditEntitiesOnCriteria(acr.ID);
                    criteriaList.Add(acr);
                    }
                }
            con.Dispose();
            return criteriaList;
            }
        public List<AuditCriteriaModel> GetPostChangesAuditCriterias()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditCriteriaModel> criteriaList = new List<AuditCriteriaModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {

                cmd.CommandText = "pkg_pg.P_GetPostChangesAuditCriterias";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditCriteriaModel acr = new AuditCriteriaModel();
                    acr.ID = Convert.ToInt32(rdr["ID"]);
                    acr.ENTITY_TYPEID = Convert.ToInt32(rdr["ENTITY_TYPEID"]);
                    if (rdr["SIZE_ID"].ToString() != null && rdr["SIZE_ID"].ToString() != "")
                        acr.SIZE_ID = Convert.ToInt32(rdr["SIZE_ID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        acr.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    acr.RISK_ID = Convert.ToInt32(rdr["RISK_ID"]);
                    acr.FREQUENCY_ID = Convert.ToInt32(rdr["FREQUENCY_ID"]);
                    acr.NO_OF_DAYS = Convert.ToInt32(rdr["NO_OF_DAYS"]);
                    acr.APPROVAL_STATUS = Convert.ToInt32(rdr["APPROVAL_STATUS"]);
                    acr.AUDITPERIODID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    acr.PERIOD = rdr["PERIOD"].ToString();
                    acr.ENTITY = rdr["ENTITY"].ToString();
                    acr.FREQUENCY = rdr["FREQUENCY"].ToString();
                    acr.SIZE = rdr["BRSIZE"].ToString();
                    acr.RISK = rdr["RISK"].ToString();
                    acr.VISIT = rdr["VISIT"].ToString();
                    acr.ENTITY_NAME = rdr["NAME"].ToString();
                    acr.COMMENTS = rdr["REMARKS"].ToString();// this.GetAuditCriteriaLogLastStatus(acr.ID);
                    criteriaList.Add(acr);
                    }
                }
            con.Dispose();
            return criteriaList;
            }
        public List<AuditeeEntitiesModel> GetAuditeeEntitiesForOutstandingParas(int ENTITY_CODE = 0)
            {
            //Functionality Completed, no further need of this code is required as of now...
            // Once Page will be removed, this function will be removed as well
            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            return entitiesList;
            }
        public string GeneratePlanForAuditCriteria(int CRITERIA_ID)
            {
            string resMsg = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.Tentative_Audit_Plan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("CRITERIA_ID", OracleDbType.Int32).Value = CRITERIA_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    resMsg = rdr["REMARKS"].ToString();
                    }

                }
            con.Dispose();
            return resMsg;
            }
        public List<AuditEntitiesModel> GetAuditEntities()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditEntitiesModel> entitiesList = new List<AuditEntitiesModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditEntities";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditEntitiesModel entity = new AuditEntitiesModel();
                    entity.AUTID = Convert.ToInt32(rdr["AUTID"]);
                    entity.ENTITYCODE = rdr["ENTITYCODE"].ToString();
                    entity.ENTITYTYPEDESC = rdr["ENTITYTYPEDESC"].ToString();
                    entity.AUDITABLE = rdr["AUDITABLE"].ToString();
                    entity.D_RISK = rdr["d_risk"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditeeEntitiesModel> GetEntitiesByParentEntityTypeId(int ENTITY_TYPE_ID = 0)
            {
            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditeeEntities";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("TYPEID", OracleDbType.Int32).Value = ENTITY_TYPE_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    if (rdr["ENTITY_ID"].ToString() != "" && rdr["ENTITY_ID"].ToString() != null)
                        entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);

                    if (rdr["name"].ToString() != "" && rdr["name"].ToString() != null)
                        entity.NAME = rdr["name"].ToString();

                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditEmployeeModel> GetAuditEmployees(int dept_code = 0)
            {
            var con = this.DatabaseConnection(); con.Open();
            List<AuditEmployeeModel> empList = new List<AuditEmployeeModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditEmployees";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("dept_code", OracleDbType.Int32).Value = dept_code;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditEmployeeModel emp = new AuditEmployeeModel();
                    emp.PPNO = Convert.ToInt32(rdr["PPNO"]);
                    emp.DEPARTMENTCODE = Convert.ToInt32(rdr["DEPARTMENTCODE"]);
                    emp.RANKCODE = Convert.ToInt32(rdr["RANKCODE"]);
                    emp.DESIGNATIONCODE = Convert.ToInt32(rdr["DESIGNATIONCODE"]);

                    emp.DEPTARMENT = rdr["DEPTARMENT"].ToString();
                    emp.EMPLOYEEFIRSTNAME = rdr["EMPLOYEEFIRSTNAME"].ToString();
                    emp.EMPLOYEELASTNAME = rdr["EMPLOYEELASTNAME"].ToString();
                    emp.CURRENT_RANK = rdr["CURRENT_RANK"].ToString();
                    emp.FUN_DESIGNATION = rdr["FUN_DESIGNATION"].ToString();
                    emp.TYPE = rdr["TYPE"].ToString();
                    empList.Add(emp);
                    }
                }
            con.Dispose();
            return empList;
            }
        public List<TentativePlanModel> GetTentativePlansForFields(bool sessionCheck = true)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<TentativePlanModel> tplansList = new List<TentativePlanModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                string _sql = "pkg_pg.p_get_audit_plan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandText = _sql;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    TentativePlanModel tplan = new TentativePlanModel();
                    tplan.PLAN_ID = Convert.ToInt32(rdr["PLAN_ID"]);
                    tplan.CRITERIA_ID = Convert.ToInt32(rdr["CRITERIA_ID"]);
                    tplan.AUDIT_PERIOD_ID = Convert.ToInt32(rdr["AUDITPERIODID"]);
                    tplan.AUDITEDBY = Convert.ToInt32(rdr["AUDITEDBY"]);
                    tplan.BR_SIZE = rdr["AUDITEE_SIZE"].ToString();
                    tplan.RISK = rdr["AUDITEE_RISK"].ToString();
                    tplan.NATURE_OF_AUDIT = rdr["NATURE_OF_AUDIT"].ToString();
                    tplan.NO_OF_DAYS = Convert.ToInt32(rdr["NO_OF_DAYS"]);
                    tplan.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    tplan.CODE = rdr["ENTITY_CODE"].ToString();
                    tplan.ENTITY_TYPE_ID = Convert.ToInt32(rdr["ENTITY_TYPE_ID"].ToString());
                    tplan.ENTITY_NAME = rdr["AUDITEE_NAME"].ToString();
                    tplan.FREQUENCY_DESCRIPTION = rdr["FREQUENCY_DISCRIPTION"].ToString();
                    tplan.PERIOD_NAME = rdr["PERIOD_NAME"].ToString();
                    tplan.REPORTING_OFFICE = rdr["REPORTING_OFFICE"].ToString();
                    tplan.ENT_TYPE = rdr["ent_type"].ToString();
                    tplansList.Add(tplan);
                    }
                }
            con.Dispose();
            return tplansList;
            }
        public string GetAuditOperationalStartDate(int auditPeriodId = 0, int entityCode = 0)
            {
            string result = "";
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditOperationalStartDate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("entityCode", OracleDbType.Int32).Value = entityCode;
                cmd.Parameters.Add("auditPeriodId", OracleDbType.Int32).Value = auditPeriodId;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    result = rdr["YEAR"].ToString() + "-";
                    result += rdr["MONTH"].ToString() + "-";
                    result += rdr["DAY"].ToString();
                    }
                }
            con.Dispose();
            return result;
            }
        public List<AuditRefEngagementPlanModel> GetAuditEngagementPlans()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditRefEngagementPlanModel> list = new List<AuditRefEngagementPlanModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetAuditEngagementPlans";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader ardr = cmd.ExecuteReader();
                while (ardr.Read())
                    {
                    AuditRefEngagementPlanModel eng = new AuditRefEngagementPlanModel();
                    eng.ENG_ID = Convert.ToInt32(ardr["eng_id"].ToString());
                    eng.TEAM_NAME = ardr["team_name"].ToString();
                    eng.ENTITY_NAME = ardr["name"].ToString();
                    eng.AUDIT_STARTDATE = Convert.ToDateTime(ardr["audit_startdate"].ToString()).ToString("dd/MM/yyyy");
                    eng.AUDIT_ENDDATE = Convert.ToDateTime(ardr["audit_enddate"].ToString()).ToString("dd/MM/yyyy");
                    eng.OP_STARTDATE = Convert.ToDateTime(ardr["op_startdate"].ToString()).ToString("dd/MM/yyyy");
                    eng.OP_ENDDATE = Convert.ToDateTime(ardr["op_enddate"].ToString()).ToString("dd/MM/yyyy");
                    eng.ENTITY_ID = Convert.ToInt32(ardr["entity_id"].ToString());
                    list.Add(eng);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<AuditRefEngagementPlanModel> GetRefferedBackAuditEngagementPlans()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditRefEngagementPlanModel> list = new List<AuditRefEngagementPlanModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetRefferedBackAuditEngagementPlans";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader ardr = cmd.ExecuteReader();
                while (ardr.Read())
                    {
                    AuditRefEngagementPlanModel eng = new AuditRefEngagementPlanModel();
                    eng.PLAN_ID = Convert.ToInt32(ardr["plan_id"].ToString());
                    eng.ENG_ID = Convert.ToInt32(ardr["eng_id"].ToString());
                    eng.TEAM_NAME = ardr["team_name"].ToString();
                    eng.TEAM_ID = Convert.ToInt32(ardr["team_id"].ToString());
                    eng.ENTITY_NAME = ardr["name"].ToString();
                    eng.COMMENTS = this.GetLatestCommentsOnEngagement(Convert.ToInt32(eng.ENG_ID)).ToString();
                    eng.AUDIT_STARTDATE = Convert.ToDateTime(ardr["audit_startdate"].ToString()).ToString("dd/MM/yyyy");
                    eng.AUDIT_ENDDATE = Convert.ToDateTime(ardr["audit_enddate"].ToString()).ToString("dd/MM/yyyy");
                    eng.OP_STARTDATE = Convert.ToDateTime(ardr["op_startdate"].ToString()).ToString("dd/MM/yyyy");
                    eng.OP_ENDDATE = Convert.ToDateTime(ardr["op_enddate"].ToString()).ToString("dd/MM/yyyy");

                    eng.ENTITY_ID = Convert.ToInt32(ardr["entity_id"].ToString());
                    list.Add(eng);
                    }
                }
            con.Dispose();
            return list;
            }
        public AuditEngagementPlanModel AddAuditEngagementPlan(AuditEngagementPlanModel ePlan)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            ePlan.CREATED_ON = System.DateTime.Now;
            int placeofposting = Convert.ToInt32(loggedInUser.UserEntityID);
            bool isContinue = false;

            ePlan.CREATEDBY = Convert.ToInt32(loggedInUser.PPNumber);
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_AddAuditEngagementPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("PERIODID", OracleDbType.Int32).Value = ePlan.PERIOD_ID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ePlan.ENTITY_ID;
                cmd.Parameters.Add("AUDIT_STARTDATE", OracleDbType.Date).Value = ePlan.AUDIT_STARTDATE;
                cmd.Parameters.Add("CREATEDBY", OracleDbType.Int32).Value = ePlan.CREATEDBY;
                cmd.Parameters.Add("AUDIT_ENDDATE", OracleDbType.Date).Value = ePlan.AUDIT_ENDDATE;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = ePlan.STATUS;
                cmd.Parameters.Add("TEAMID", OracleDbType.Int32).Value = ePlan.TEAM_ID;
                cmd.Parameters.Add("TEAM_NAME", OracleDbType.Varchar2).Value = ePlan.TEAM_NAME;
                cmd.Parameters.Add("PLANID", OracleDbType.Int32).Value = ePlan.PLAN_ID;
                cmd.Parameters.Add("OP_STARTDATE", OracleDbType.Date).Value = ePlan.OP_STARTDATE;
                cmd.Parameters.Add("OP_ENDDATE", OracleDbType.Date).Value = ePlan.OP_ENDDATE;
                cmd.Parameters.Add("TRAVELDAY", OracleDbType.Int32).Value = ePlan.TRAVELDAY;
                cmd.Parameters.Add("RRDAY", OracleDbType.Int32).Value = ePlan.RRDAY;
                cmd.Parameters.Add("D_Day", OracleDbType.Int32).Value = ePlan.D_Day;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ePlan.REMARKS_OUT = rdr["REMARKS"].ToString();
                    if (rdr["REF"].ToString() != "" && rdr["REF"].ToString() != null && rdr["REF"].ToString() == "1")
                        {
                        isContinue = true;
                        ePlan.IS_SUCCESS = "Yes";
                        }
                    }

                if (isContinue)
                    {
                    cmd.CommandText = "pkg_pg.P_AddAuditteamtasklist";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("TEAMID", OracleDbType.Int32).Value = ePlan.TEAM_ID;
                    cmd.Parameters.Add("PLANID", OracleDbType.Int32).Value = ePlan.PLAN_ID;
                    cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ePlan.ENTITY_ID;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.ExecuteReader();

                    }

                }
            con.Dispose();
            return ePlan;

            }
        public bool RefferedBackAuditEngagementPlan(int ENG_ID, string REMARKS)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_RefferedBackAuditEngagementPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.ExecuteReader();
                }
            con.Dispose();
            return true;
            }
        public string RerecommendAuditEngagementPlan(int ENG_ID, int PLAN_ID, int ENTITY_ID, DateTime OP_START_DATE, DateTime OP_END_DATE, DateTime START_DATE, DateTime END_DATE, int TEAM_ID, string COMMENTS)
            {
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_RerecommendAuditEngagementPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("STARTDATE", OracleDbType.Date).Value = START_DATE;
                cmd.Parameters.Add("ENDDATE", OracleDbType.Date).Value = END_DATE;
                cmd.Parameters.Add("TEAMID", OracleDbType.Int32).Value = TEAM_ID;
                cmd.Parameters.Add("PLANID", OracleDbType.Int32).Value = PLAN_ID;
                cmd.Parameters.Add("OP_STARTDATE", OracleDbType.Date).Value = OP_START_DATE;
                cmd.Parameters.Add("OP_ENDDATE", OracleDbType.Date).Value = OP_END_DATE;
                cmd.Parameters.Add("REMARKS", OracleDbType.Varchar2).Value = COMMENTS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["REMARK"].ToString();
                    }
                }
            con.Dispose();
            return resp;
            }
        public bool ApproveAuditEngagementPlan(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_ApproveAuditEngagementPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.ExecuteReader();

                }
            con.Dispose();
            return true;
            }

        public List<AuditFrequencyModel> GetAuditFrequencies()
            {
            var con = this.DatabaseConnection(); con.Open();
            List<AuditFrequencyModel> freqList = new List<AuditFrequencyModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.p_GetAuditFrequencies";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditFrequencyModel freq = new AuditFrequencyModel();
                    freq.ID = Convert.ToInt32(rdr["ID"]);
                    freq.FREQUENCY_ID = Convert.ToInt32(rdr["FREQUENCY_ID"]);
                    freq.FREQUENCY_DISCRIPTION = rdr["FREQUENCY_DISCRIPTION"].ToString();
                    freq.STATUS = rdr["STATUS"].ToString();
                    freqList.Add(freq);
                    }
                }
            con.Dispose();
            return freqList;
            }

        public string GetLatestCommentsOnEngagement(int engId = 0)
            {
            var con = this.DatabaseConnection(); con.Open();
            string response = "";
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_pg.P_GetLatestCommentsOnEngagement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = engId;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    response = rdr["remarks"].ToString();
                    }
                }
            con.Dispose();
            return response;
            }


        }
    }
