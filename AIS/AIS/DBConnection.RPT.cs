using AIS.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace AIS.Controllers
    {
    public partial class DBConnection
        {
        public List<AuditPeriodModel> GetAllParasYears()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPeriodModel> periodList = new List<AuditPeriodModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GetAllParasYear";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditPeriodModel period = new AuditPeriodModel();
                    period.AUDITPERIODID = Convert.ToInt32(rdr["audit_period"]);
                    periodList.Add(period);
                    }
                }
            con.Dispose();
            return periodList;
            }
        }

        public List<DepartmentModel> GetDepartments(int div_code = 0, bool sessionCheck = true)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            List<DepartmentModel> deptList = new List<DepartmentModel>();
            var loggedInUser = sessionHandler.GetSessionUser();

            var entityId = 0;
            if (div_code == 0)
                entityId = Convert.ToInt32(loggedInUser.UserEntityID);
            else
                entityId = div_code;

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_GetDepartments";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("EntityId", OracleDbType.Int32).Value = entityId;
                cmd.Parameters.Add("PPNUM", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    DepartmentModel dept = new DepartmentModel();
                    dept.ID = Convert.ToInt32(rdr["ID"]);
                    dept.DIV_ID = Convert.ToInt32(rdr["DIVISIONID"]);
                    dept.NAME = rdr["NAME"].ToString();
                    dept.CODE = rdr["CODE"].ToString();
                    if (rdr["ISACTIVE"].ToString() == "Y")
                        dept.STATUS = "Active";
                    else if (rdr["ISACTIVE"].ToString() == "N")
                        dept.STATUS = "InActive";
                    else
                        dept.STATUS = rdr["ISACTIVE"].ToString();
                    dept.DIV_NAME = rdr["DIV_NAME"].ToString();
                    if (rdr["AUDITED_BY_DEPID"].ToString() != null && rdr["AUDITED_BY_DEPID"].ToString() != "")
                        {
                        // dept.AUDITED_BY_NAME = rdr["ADUTIED_BY"].ToString();

                        dept.AUDITED_BY_DEPID = Convert.ToInt32(rdr["AUDITED_BY_DEPID"]);
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlParams.GetDepartmentNameByIdQueryFromParams(dept.AUDITED_BY_DEPID);
                        OracleDataReader rdr2 = cmd.ExecuteReader();
                        while (rdr2.Read())
                            {
                            dept.AUDITED_BY_NAME = rdr2["NAME"].ToString();
                            }
                        }
                    deptList.Add(dept);
                    }
                }
            con.Dispose();
            return deptList;
            }

        public List<ZoneWiseOldParasPerformanceModel> GetZoneWiseOldParasPerformance()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            string query = "";
            query = query + "  s.ID=" + loggedInUser.UserEntityID;


            List<ZoneWiseOldParasPerformanceModel> list = new List<ZoneWiseOldParasPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GetZoneWiseOldParasPerformance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ZoneWiseOldParasPerformanceModel chk = new ZoneWiseOldParasPerformanceModel();
                    chk.ZONEID = rdr["ID"].ToString();
                    chk.ZONENAME = rdr["ZONENAME"].ToString();
                    chk.PARA_ENTERED = rdr["PARA_ENTERED"].ToString();
                    chk.PARA_PENDING = rdr["PARA_PENDING"].ToString();
                    chk.PARA_TOTAL = rdr["PARA_TOTAL"].ToString();

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public ActiveInactiveChart GetActiveInactiveChartData()
            {
            var con = this.DatabaseConnection(); con.Open();
            ActiveInactiveChart chk = new ActiveInactiveChart();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GetActiveInactiveChartData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    if (rdr["STATUS"].ToString() == "1")
                        chk.Active_Count = rdr["TOTAL_COUNT"].ToString();
                    if (rdr["STATUS"].ToString() == "0")
                        chk.Inactive_Count = rdr["TOTAL_COUNT"].ToString();
                    }
                }
            con.Dispose();
            return chk;
            }

        public List<UserRelationshipModel> GetchildpostingForParaPositionReport(int e_r_id = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            if (e_r_id == 0)
                e_r_id = Convert.ToInt32(loggedInUser.UserEntityID);

            List<UserRelationshipModel> entitiesList = new List<UserRelationshipModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_Getchildposting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("erid", OracleDbType.Int32).Value = e_r_id;
                cmd.Parameters.Add("USER_ENTITY_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    UserRelationshipModel entity = new UserRelationshipModel();
                    entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    entity.C_NAME = rdr["C_NAME"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<UserRelationshipModel> GetparentrepofficeForParaPositionReport(int r_id = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<UserRelationshipModel> entitiesList = new List<UserRelationshipModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_Getparentrepoffice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("rid", OracleDbType.Int32).Value = r_id;
                cmd.Parameters.Add("user_entity_id", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    UserRelationshipModel entity = new UserRelationshipModel();
                    entity.ENTITY_REALTION_ID = Convert.ToInt32(rdr["ENTITY_REALTION_ID"]);
                    entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    entity.ACTIVE = rdr["ACTIVE"].ToString();
                    entity.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    entity.ENTITYTYPEDESC = rdr["ENTITYTYPEDESC"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditeeEntitiesModel> GetAuditDepartmentsZones()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                //AuditDepartmentList
                cmd.CommandText = "pkg_rpt.P_AUDITED_BY_DEPARTMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["NAME"].ToString();
                    entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<UserRelationshipModel> GetrealtionshiptypeForParaPositionReport()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<UserRelationshipModel> entitiesList = new List<UserRelationshipModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_Getrealtionshiptype";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserRoleid", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    UserRelationshipModel entity = new UserRelationshipModel();
                    entity.ENTITY_REALTION_ID = Convert.ToInt32(rdr["ENTITY_REALTION_ID"]);
                    entity.FIELD_NAME = rdr["FIELD_NAME"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditPlanEngagementModel> GetAuditPlanEngagement(int periodid)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPlanEngagementModel> periodList = new List<AuditPlanEngagementModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.r_audit_plan_engagement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("Audit_Period", OracleDbType.Int32).Value = periodid;

                cmd.Parameters.Add("auditbyid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;


                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditPlanEngagementModel period = new AuditPlanEngagementModel();



                    period.AUDITPERIOD = rdr["AUDITPERIOD"].ToString();
                    period.PARENT_OFFICE = rdr["PARENT_OFFICE"].ToString();
                    period.ENITIY_NAME = rdr["ENITIY_NAME"].ToString();
                    period.PARENT_OFFICE = rdr["PARENT_OFFICE"].ToString();

                    period.AUDIT_STARTDATE = Convert.ToDateTime(rdr["AUDIT_STARTDATE"]);

                    period.AUDIT_ENDDATE = Convert.ToDateTime(rdr["AUDIT_ENDDATE"]);
                    if (rdr["TRAVEL_DAY"].ToString() != null && rdr["TRAVEL_DAY"].ToString() != "")
                        period.TRAVEL_DAY = Convert.ToInt32(rdr["TRAVEL_DAY"]);
                    if (rdr["REVENUE_RECORD_DAY"].ToString() != null && rdr["REVENUE_RECORD_DAY"].ToString() != "")
                        period.REVENUE_RECORD_DAY = Convert.ToInt32(rdr["REVENUE_RECORD_DAY"]);
                    if (rdr["DISCUSSION_DAY"].ToString() != null && rdr["DISCUSSION_DAY"].ToString() != "")
                        period.DISCUSSION_DAY = Convert.ToInt32(rdr["DISCUSSION_DAY"]);

                    period.TEAM_NAME = rdr["TEAM_NAME"].ToString();
                    // period.MEMBER_NAME = rdr["MEMBER_NAME"].ToString();
                    period.STATUS = rdr["STATUS"].ToString();


                    periodList.Add(period);
                    }
                }
            con.Dispose();
            return periodList;
            }

        public List<JoiningCompletionReportModel> GetJoiningCompletion(int DEPT_ID, DateTime AUDIT_STARTDATE, DateTime AUDIT_ENDDATE)
            {
            List<JoiningCompletionReportModel> list = new List<JoiningCompletionReportModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_JOININGCOMPLETION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("DEPT_ID", OracleDbType.Int32).Value = DEPT_ID;
                cmd.Parameters.Add("AUDIT_START", OracleDbType.Date).Value = AUDIT_STARTDATE;
                cmd.Parameters.Add("AUDIT_END", OracleDbType.Date).Value = AUDIT_ENDDATE;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    JoiningCompletionReportModel jc = new JoiningCompletionReportModel();

                    jc.AUDIT_BY = rdr["AUDIT_BY"].ToString();
                    jc.AUDITEE_NAME = rdr["AUDITEE_NAME"].ToString();
                    jc.TEAM_NAME = rdr["TEAM_NAME"].ToString();
                    jc.PPNO = Convert.ToInt32(rdr["PPNO"].ToString());
                    jc.NAME = rdr["NAME"].ToString();
                    jc.TEAM_LEAD = rdr["TEAM_LEAD"].ToString();
                    jc.JOINING_DATE = Convert.ToDateTime(rdr["JOINING_DATE"].ToString());
                    jc.COMPLETION_DATE = Convert.ToDateTime(rdr["COMPLETION_DATE"].ToString());
                    jc.STATUS = rdr["STATUS"].ToString();

                    list.Add(jc);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<AuditPlanCompletionReportModel> GetauditplanCompletion(int DEPT_ID)
            {
            List<AuditPlanCompletionReportModel> list = new List<AuditPlanCompletionReportModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_AUDITPLANPROGRESS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("DEPT_ID", OracleDbType.Int32).Value = DEPT_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditPlanCompletionReportModel jac = new AuditPlanCompletionReportModel();

                    jac.AUDITNAME = rdr["AUDITNAME"].ToString();
                    jac.AUDITS = Convert.ToInt32(rdr["AUDITS"].ToString());
                    jac.ENGPLAN = Convert.ToInt32(rdr["ENGPLAN"].ToString());
                    jac.JOINING = Convert.ToInt32(rdr["JOINING"].ToString());
                    jac.COMPLETED = Convert.ToInt32(rdr["COMPLETED"].ToString());
                    jac.OBSERVATIONS = Convert.ToInt32(rdr["OBSERVATIONS"].ToString());
                    jac.HIGHRISKPARA = Convert.ToInt32(rdr["HIGHRISKPARA"].ToString());
                    jac.MEDIUMRISKPARA = Convert.ToInt32(rdr["MEDIUMRISKPARA"].ToString());
                    jac.LOWRISKPARA = Convert.ToInt32(rdr["LOWRISKPARA"].ToString());
                    list.Add(jac);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<CurrentAuditProgress> GetCurrentAuditProgressEntities()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<CurrentAuditProgress> list = new List<CurrentAuditProgress>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_GetAuditees";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("PP_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    CurrentAuditProgress ent = new CurrentAuditProgress();
                    ent.CODE = rdr["ENG_ID"].ToString();
                    ent.NAME = rdr["Entity_Name"].ToString();
                    list.Add(ent);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<CurrentAuditProgress> GetCurrentAuditProgress(int ENTITY_ID = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<CurrentAuditProgress> list = new List<CurrentAuditProgress>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_GetAuditeesobervations";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    CurrentAuditProgress ent = new CurrentAuditProgress();

                    ent.CODE = rdr["code"].ToString();
                    ent.NAME = rdr["auditee"].ToString();
                    ent.AREA = rdr["area"].ToString();
                    ent.OBS_COUNT = Convert.ToInt32(rdr["observation"].ToString());
                    list.Add(ent);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<CurrentActiveUsers> GetCurrentActiveUsers()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<CurrentActiveUsers> list = new List<CurrentActiveUsers>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_LOGINUSERS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    CurrentActiveUsers ent = new CurrentActiveUsers();
                    //e.code, e.name as auditee, d.heading as area, count(o.id) as observation

                    ent.DEPARTMENT_NAME = rdr["DEPTNAME"].ToString();
                    ent.NAME = rdr["EMPNAME"].ToString();
                    ent.PP_NUMBER = Convert.ToInt32(rdr["PPNO"].ToString());
                    ent.LOGGED_IN_DATE = Convert.ToDateTime(rdr["LOGINDATE"].ToString());
                    ent.SESSION_TIME = rdr["SESSIONTIME"].ToString();
                    list.Add(ent);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<ParaTextModel> GetReportParas(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ParaTextModel> list = new List<ParaTextModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.r_getauditeeParas";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ParaTextModel chk = new ParaTextModel();
                    chk.MEMO_NO = Convert.ToInt32(rdr["memo_number"]);

                    chk.MEMO_TXT = rdr["text"].ToString();

                    list.Add(chk);

                    }
                }
            con.Dispose();
            return list;
            }

        public List<ZoneBranchParaStatusModel> GetZoneBranchParaPositionStatus(int Entity_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ZoneBranchParaStatusModel> list = new List<ZoneBranchParaStatusModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GetParaPositionForZoneBranches";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("userid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("EntityId", OracleDbType.Int32).Value = Entity_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ZoneBranchParaStatusModel zb = new ZoneBranchParaStatusModel();
                    zb.Entity_Name = rdr["Branch_Name"].ToString();
                    zb.Total_Paras = Convert.ToInt32(rdr["Total_Paras"].ToString());
                    zb.Settled_Paras = Convert.ToInt32(rdr["Setteled_para"].ToString());
                    zb.Unsettled_Paras = Convert.ToInt32(rdr["UnSetteled_para"].ToString());
                    list.Add(zb);

                    }
                }
            con.Dispose();
            return list;
            }

        public List<AuditPlanReportModel> getauditplanreport()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPlanReportModel> planList = new List<AuditPlanReportModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {

                string _sql = "pkg_rpt.r_eng_plan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = "112312";
                cmd.Parameters.Add("entityid", OracleDbType.Int32).Value = "112696";
                cmd.Parameters.Add("azone", OracleDbType.Int32).Value = "112928";
                cmd.Parameters.Add("risk_rating", OracleDbType.Int32).Value = "2";
                cmd.Parameters.Add("branch_size", OracleDbType.Int32).Value = "3";


                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandText = _sql;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditPlanReportModel plan = new AuditPlanReportModel();



                    plan.AUDITEDBY = rdr["AUDITEDBY"].ToString();
                    plan.PARRENTOFFICE = rdr["PARRENTOFFICE"].ToString();
                    plan.AUDITEENAME = rdr["AUDITEENAME"].ToString();
                    plan.LASTAUDITOPSENDATE = rdr["LASTAUDITOPSENDATE"].ToString();

                    plan.ENTITYRISK = rdr["ENTITYRISK"].ToString();
                    plan.ENTITYSIZE = rdr["ENTITYSIZE"].ToString();
                    plan.AUDITEENAME = rdr["AUDITEENAME"].ToString();
                    plan.LASTAUDITOPSENDATE = rdr["LASTAUDITOPSENDATE"].ToString();

                    if (rdr["ENTITYCODE"].ToString() != null && rdr["ENTITYCODE"].ToString() != "")
                        plan.ENTITYCODE = Convert.ToInt32(rdr["ENTITYCODE"]);
                    if (rdr["ANTITYID"].ToString() != null && rdr["ANTITYID"].ToString() != "")
                        plan.ANTITYID = Convert.ToInt32(rdr["ANTITYID"]);
                    if (rdr["NORMALDAYS"].ToString() != null && rdr["NORMALDAYS"].ToString() != "")
                        plan.NORMALDAYS = Convert.ToInt32(rdr["NORMALDAYS"]);
                    if (rdr["REVENUEDAYS"].ToString() != null && rdr["REVENUEDAYS"].ToString() != "")
                        plan.REVENUEDAYS = Convert.ToInt32(rdr["REVENUEDAYS"]);
                    if (rdr["TRAVELDAY"].ToString() != null && rdr["TRAVELDAY"].ToString() != "")
                        plan.TRAVELDAY = Convert.ToInt32(rdr["TRAVELDAY"]);
                    if (rdr["DISCUSSIONDAY"].ToString() != null && rdr["DISCUSSIONDAY"].ToString() != "")
                        plan.DISCUSSIONDAY = Convert.ToInt32(rdr["DISCUSSIONDAY"]);

                    plan.AUDITSTARTDATE = rdr["AUDITSTARTDATE"].ToString();
                    plan.AUDITENDDATE = rdr["AUDITENDDATE"].ToString();
                    plan.TNAME = rdr["TNAME"].ToString();
                    plan.TEAMLEAD = rdr["TEAMLEAD"].ToString();

                    planList.Add(plan);
                    }
                }
            con.Dispose();
            return planList;
            }

        public List<AuditeeAddressModel> GetAddress(int ENT_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeAddressModel> list = new List<AuditeeAddressModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.r_getauditeeaddress";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("EngId", OracleDbType.Int32).Value = ENT_ID;
                cmd.Parameters.Add("ppnum", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeAddressModel chk = new AuditeeAddressModel();
                    chk.ENG_ID = Convert.ToInt32(rdr["ENG_ID"]);
                    chk.CODE = Convert.ToInt32(rdr["CODE"]);


                    chk.P_NAME = rdr["P_NAME"].ToString();
                    chk.ADDRESS = rdr["ADDRESS"].ToString();
                    chk.NAME = rdr["NAME"].ToString();
                    chk.LICENSE = rdr["LICENSE"].ToString();
                    if (rdr["DATE_OF_OPENING"].ToString() != null && rdr["DATE_OF_OPENING"].ToString() != "")
                        chk.DATE_OF_OPENING = Convert.ToDateTime(rdr["DATE_OF_OPENING"].ToString()).ToString("dd/MM/yyyy");
                    if (rdr["AUDIT_STARTDATE"].ToString() != null && rdr["AUDIT_STARTDATE"].ToString() != "")
                        chk.AUDIT_STARTDATE = Convert.ToDateTime(rdr["AUDIT_STARTDATE"].ToString()).ToString("dd/MM/yyyy");
                    if (rdr["AUDIT_ENDDATE"].ToString() != null && rdr["AUDIT_ENDDATE"].ToString() != "")
                        chk.AUDIT_ENDDATE = Convert.ToDateTime(rdr["AUDIT_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    if (rdr["OPERATION_STARTDATE"].ToString() != null && rdr["OPERATION_STARTDATE"].ToString() != "")
                        chk.OPERATION_STARTDATE = Convert.ToDateTime(rdr["OPERATION_STARTDATE"].ToString()).ToString("dd/MM/yyyy");
                    if (rdr["OPERATION_ENDDATE"].ToString() != null && rdr["OPERATION_ENDDATE"].ToString() != "")
                        chk.OPERATION_ENDDATE = Convert.ToDateTime(rdr["OPERATION_ENDDATE"].ToString()).ToString("dd/MM/yyyy");


                    chk.HIGH = rdr["HIGH"].ToString().Trim() == "" ? "0" : rdr["HIGH"].ToString();
                    chk.MEDIUM = rdr["MEDIUM"].ToString().Trim() == "" ? "0" : rdr["MEDIUM"].ToString();
                    chk.LOW = rdr["LOW"].ToString().Trim() == "" ? "0" : rdr["LOW"].ToString();

                    chk.SETTLED_HIGH = rdr["SETTLE_HIGH"].ToString().Trim() == "" ? "0" : rdr["SETTLE_HIGH"].ToString();
                    chk.SETTLED_MEDIUM = rdr["SETTLE_MEDIUM"].ToString().Trim() == "" ? "0" : rdr["SETTLE_MEDIUM"].ToString();
                    chk.SETTLED_LOW = rdr["SETTLE_LOW"].ToString().Trim() == "" ? "0" : rdr["SETTLE_LOW"].ToString();

                    chk.OPEN_HIGH = rdr["OPEN_HIGH"].ToString().Trim() == "" ? "0" : rdr["OPEN_HIGH"].ToString();
                    chk.OPEN_MEDIUM = rdr["OPEN_MEDIUM"].ToString().Trim() == "" ? "0" : rdr["OPEN_MEDIUM"].ToString();
                    chk.OPEN_LOW = rdr["OPEN_LOW"].ToString().Trim() == "" ? "0" : rdr["OPEN_LOW"].ToString();

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<GetAuditeeParasModel> GetAuditeReportStatus(int eng_id)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();

            List<GetAuditeeParasModel> list = new List<GetAuditeeParasModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.r_getauditeeparas";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("engid", OracleDbType.Int32).Value = eng_id;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    GetAuditeeParasModel chk = new GetAuditeeParasModel();

                    chk.MESSAGE = rdr["MESSAGE"].ToString();
                    chk.REF_OUT = rdr["REF_OUT"].ToString();



                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<GetTeamDetailsModel> GetTeamDetails(int eng_id)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetTeamDetailsModel> list = new List<GetTeamDetailsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_getauditteams";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = eng_id;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    GetTeamDetailsModel chk = new GetTeamDetailsModel();

                    chk.TEAM_NAME = rdr["TEAM_NAME"].ToString();
                    chk.MEMBER_PPNO = rdr["MEMBER_PPNO"].ToString();
                    chk.MEMBER_NAME = rdr["MEMBER_NAME"].ToString();
                    chk.ISTEAMLEAD = rdr["ISTEAMLEAD"].ToString();
                    chk.AUDIT_START_DATE = Convert.ToDateTime(rdr["AUDIT_START_DATE"].ToString()).ToString("dd/MM/yyyy");
                    chk.AUDIT_END_DATE = Convert.ToDateTime(rdr["AUDIT_END_DATE"].ToString()).ToString("dd/MM/yyyy");

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<GetFinalReportModel> GetAuditeeParas(int eng_id)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetFinalReportModel> list = new List<GetFinalReportModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.r_getauditeeparas";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("engid", OracleDbType.Int32).Value = 1198;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    GetFinalReportModel chk = new GetFinalReportModel();


                    chk.MEMO_NUMBER = rdr["MEMO_NUMBER"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    //chk.STATUS = rdr["STATUS"].ToString();
                    chk.V_HEADER = rdr["V_HEADER"].ToString();
                    chk.V_DETAIL = rdr["V_DETAIL"].ToString();
                    chk.RISK = rdr["RISK"].ToString();
                    chk.OBSERVATION = rdr["OBSERVATION"].ToString();
                    chk.MANAGEMENT_REPLY = rdr["MANAGEMENT_REPLY"].ToString();
                    chk.RECOMMENDATION = rdr["RECOMMENDATION"].ToString();
                    // chk.REMARKS = rdr["REMARKS"].ToString();
                    chk.ASSIGNEDTO = rdr["ASSIGNEDTO"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.REF_OUT = rdr["REF_OUT"].ToString();
                    chk.MESSAGE = rdr["MESSAGE"].ToString();


                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<AuditPlanReportModel> GetFadAuditPlanReport(int ent_id, int z_id, int risk, int size)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPlanReportModel> planList = new List<AuditPlanReportModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {

                string _sql = "pkg_rpt.r_eng_plan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("entityid", OracleDbType.Int32).Value = ent_id;
                cmd.Parameters.Add("azone", OracleDbType.Int32).Value = z_id;
                cmd.Parameters.Add("risk_rating", OracleDbType.Int32).Value = risk;
                cmd.Parameters.Add("branch_size", OracleDbType.Int32).Value = size;


                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandText = _sql;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditPlanReportModel plan = new AuditPlanReportModel();



                    plan.AUDITEDBY = rdr["AUDITEDBY"].ToString();
                    plan.PARRENTOFFICE = rdr["PARRENTOFFICE"].ToString();
                    plan.AUDITEENAME = rdr["AUDITEENAME"].ToString();
                    plan.LASTAUDITOPSENDATE = rdr["LASTAUDITOPSENDATE"].ToString();

                    plan.ENTITYRISK = rdr["ENTITYRISK"].ToString();
                    plan.ENTITYSIZE = rdr["ENTITYSIZE"].ToString();
                    plan.AUDITEENAME = rdr["AUDITEENAME"].ToString();
                    plan.LASTAUDITOPSENDATE = rdr["LASTAUDITOPSENDATE"].ToString();

                    if (rdr["ENTITYCODE"].ToString() != null && rdr["ENTITYCODE"].ToString() != "")
                        plan.ENTITYCODE = Convert.ToInt32(rdr["ENTITYCODE"]);
                    if (rdr["ANTITYID"].ToString() != null && rdr["ANTITYID"].ToString() != "")
                        plan.ANTITYID = Convert.ToInt32(rdr["ANTITYID"]);
                    if (rdr["NORMALDAYS"].ToString() != null && rdr["NORMALDAYS"].ToString() != "")
                        plan.NORMALDAYS = Convert.ToInt32(rdr["NORMALDAYS"]);
                    if (rdr["REVENUEDAYS"].ToString() != null && rdr["REVENUEDAYS"].ToString() != "")
                        plan.REVENUEDAYS = Convert.ToInt32(rdr["REVENUEDAYS"]);
                    if (rdr["TRAVELDAY"].ToString() != null && rdr["TRAVELDAY"].ToString() != "")
                        plan.TRAVELDAY = Convert.ToInt32(rdr["TRAVELDAY"]);
                    if (rdr["DISCUSSIONDAY"].ToString() != null && rdr["DISCUSSIONDAY"].ToString() != "")
                        plan.DISCUSSIONDAY = Convert.ToInt32(rdr["DISCUSSIONDAY"]);

                    plan.AUDITSTARTDATE = rdr["AUDITSTARTDATE"].ToString();
                    plan.AUDITENDDATE = rdr["AUDITENDDATE"].ToString();
                    plan.TNAME = rdr["TNAME"].ToString();
                    plan.TEAMLEAD = rdr["TEAMLEAD"].ToString();

                    planList.Add(plan);
                    }
                }
            con.Dispose();
            return planList;
            }

        public List<FADGetReportEntititiesModel> FADGetReportEntitities()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADGetReportEntititiesModel> list = new List<FADGetReportEntititiesModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {

                cmd.CommandText = "pkg_rpt.p_get_entities";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADGetReportEntititiesModel nm = new FADGetReportEntititiesModel();

                    nm.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    nm.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();

                    list.Add(nm);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADGetReportZonesModel> FADGetReportZones()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADGetReportZonesModel> list = new List<FADGetReportZonesModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {

                cmd.CommandText = " pkg_rpt.p_get_az";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADGetReportZonesModel nm = new FADGetReportZonesModel();

                    nm.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    nm.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();

                    list.Add(nm);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADEntitySizeModel> FADGetEntitySize()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADEntitySizeModel> list = new List<FADEntitySizeModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {

                cmd.CommandText = "pkg_rpt.p_get_entity_size";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADEntitySizeModel nm = new FADEntitySizeModel();

                    nm.ENTITY_SIZE = rdr["ENTITY_SIZE"].ToString();
                    nm.DESCRIPTION = rdr["DESCRIPTION"].ToString();

                    list.Add(nm);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADEntityRiskModel> FADGetEntityRisk()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADEntityRiskModel> list = new List<FADEntityRiskModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {

                cmd.CommandText = "pkg_rpt.p_get_risk";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADEntityRiskModel nm = new FADEntityRiskModel();

                    nm.R_ID = rdr["R_ID"].ToString();
                    nm.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    nm.RATING = rdr["RATING"].ToString();

                    list.Add(nm);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADNewParaPerformanceModel> GetFADNewParaPerformance()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADNewParaPerformanceModel> list = new List<FADNewParaPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_new_paras_performance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("userid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADNewParaPerformanceModel zb = new FADNewParaPerformanceModel();
                    zb.Audit_Zone = rdr["AUDIT_ZONE"].ToString();
                    zb.Total_Paras = rdr["Total_Paras"].ToString();
                    zb.Setteled_Para = rdr["Setteled_Para"].ToString();
                    zb.Unsetteled_Para = rdr["Unsetteled_Para"].ToString();
                    zb.Ratio = rdr["Ratio"].ToString();
                    zb.R1 = rdr["R1"].ToString();
                    zb.R2 = rdr["R2"].ToString();
                    zb.R3 = rdr["R3"].ToString();
                    list.Add(zb);

                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADNewOldParaPerformanceModel> GetFADNewOldParaPerformance(int AUDIT_ZONE_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADNewOldParaPerformanceModel> list = new List<FADNewOldParaPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_new_old_paras_performance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("userid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADNewOldParaPerformanceModel zb = new FADNewOldParaPerformanceModel();
                    zb.Audit_Zone = rdr["AUDIT_ZONE"].ToString();
                    zb.Checklist = rdr["Checklist"].ToString();
                    zb.Total_Paras = rdr["Total_Paras"].ToString();
                    zb.Setteled_Para = rdr["Setteled_Para"].ToString();
                    zb.Unsetteled_Para = rdr["Unsetteled_Para"].ToString();
                    zb.Ratio = rdr["Ratio"].ToString();
                    zb.R1 = rdr["R1"].ToString();
                    zb.R2 = rdr["R2"].ToString();
                    zb.R3 = rdr["R3"].ToString();
                    list.Add(zb);

                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADLagacyParaPerformanceModel> GetFADLagacyParaPerformance()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADLagacyParaPerformanceModel> list = new List<FADLagacyParaPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = " pkg_rpt.p_get_lagacy_paras_performance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("userid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADLagacyParaPerformanceModel zb = new FADLagacyParaPerformanceModel();
                    zb.Audit_Zone = rdr["AUDIT_ZONE"].ToString();
                    zb.Total_Paras = rdr["Total_Paras"].ToString();
                    zb.Setteled_Para = rdr["Setteled_Para"].ToString();
                    zb.Unsetteled_Para = rdr["Unsetteled_Para"].ToString();
                    zb.Ratio = rdr["Ratio"].ToString();
                    zb.R1 = rdr["R1"].ToString();
                    zb.R2 = rdr["R2"].ToString();
                    zb.R3 = rdr["R3"].ToString();
                    list.Add(zb);

                    }
                }
            con.Dispose();
            return list;
            }

        public List<LegacyZoneWiseOldParasPerformanceModel> GetLegacyZoneWiseOldParasPerformance(DateTime? FILTER_DATE)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();


            List<LegacyZoneWiseOldParasPerformanceModel> list = new List<LegacyZoneWiseOldParasPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GetZoneWiseOldParasPerformance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LegacyZoneWiseOldParasPerformanceModel chk = new LegacyZoneWiseOldParasPerformanceModel();
                    chk.ZONEID = rdr["ID"].ToString();
                    chk.ZONENAME = rdr["ZONENAME"].ToString();
                    chk.PARA_ENTERED = rdr["PARA_ENTERED"].ToString();
                    chk.PARA_PENDING = rdr["PARA_PENDING"].ToString();
                    chk.PARA_TOTAL = rdr["PARA_TOTAL"].ToString();

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<LegacyUserWiseOldParasPerformanceModel> GetLegacyUserWiseOldParasPerformance(DateTime? FILTER_DATE)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();

            List<LegacyUserWiseOldParasPerformanceModel> list = new List<LegacyUserWiseOldParasPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_Get_UserWise_OldParasPerformance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("updated_date", OracleDbType.Date).Value = FILTER_DATE;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LegacyUserWiseOldParasPerformanceModel chk = new LegacyUserWiseOldParasPerformanceModel();
                    chk.AUDIT_ZONEID = rdr["AUDIT_ZONEID"].ToString();
                    chk.ZONENAME = rdr["ZONENAME"].ToString();
                    chk.PARA_ENTERED = rdr["PARA_UPDATED"].ToString();
                    chk.PPNO = rdr["PPNO"].ToString();
                    chk.DATE = rdr["updated_on"].ToString();
                    chk.EMP_NAME = rdr["EMP_NAME"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADHOUserLegacyParaUserWiseParasPerformanceModel> GetFADHOUserLegacyParaUserWiseOldParasPerformance(DateTime? FILTER_DATE)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();

            List<FADHOUserLegacyParaUserWiseParasPerformanceModel> list = new List<FADHOUserLegacyParaUserWiseParasPerformanceModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_Get_FAD_UserWise_OldParasPerformance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("pp_no", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("updated_date", OracleDbType.Date).Value = FILTER_DATE;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADHOUserLegacyParaUserWiseParasPerformanceModel chk = new FADHOUserLegacyParaUserWiseParasPerformanceModel();
                    chk.PP_NO = rdr["PPNO"].ToString();
                    chk.EMP_NAME = rdr["EMP_NAME"].ToString();
                    chk.PARA_REVIEWED = rdr["PARA_REVIEWED"].ToString();
                    chk.PARA_UPDATED = rdr["PARA_UPDATED"].ToString();
                    chk.PARA_UPDATED_WITHOUT_CHANGE = rdr["PARA_UPDATED_WITHOUT_CHANGES"].ToString();
                    chk.PARA_REFERRED_BACK = rdr["Refer_Back"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<ParaPositionReportModel> GetParaPositionReport(int P_ID = 0, int C_ID = 0)

            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<ParaPositionReportModel> list = new List<ParaPositionReportModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_para_position";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = P_ID;
                cmd.Parameters.Add("C_ID", OracleDbType.Int32).Value = C_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ParaPositionReportModel zb = new ParaPositionReportModel();
                    zb.Total_Paras = rdr["Total_Para"].ToString();
                    zb.Setteled_Para = rdr["Setteled_Para"].ToString();
                    zb.Unsetteled_Para = rdr["Un_setteled_Para"].ToString();
                    zb.P_NAME = rdr["P_NAME"].ToString();
                    zb.C_NAME = rdr["C_NAME"].ToString();
                    zb.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();

                    list.Add(zb);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<ParaPositionDetailsModel> GetParaPositionParaDetails(int ENTITY_ID = 0, int AUDIT_PERIOD = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ParaPositionDetailsModel> list = new List<ParaPositionDetailsModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_para_position_details";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("period", OracleDbType.Int32).Value = AUDIT_PERIOD;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ParaPositionDetailsModel zb = new ParaPositionDetailsModel();
                    zb.REMARKS = rdr["remarks"].ToString();
                    zb.PARA_STATUS = rdr["para_status"].ToString();
                    zb.PARA_NO = rdr["para_no"].ToString();
                    zb.HEADING = rdr["gist_of_paras"].ToString();
                    zb.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    list.Add(zb);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<FADNewOldParaPerformanceModel> GetTotalParasDetailsHO(int ENTITY_ID = 0, int PROCESS_ID = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<FADNewOldParaPerformanceModel> pdetails = new List<FADNewOldParaPerformanceModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_Dash_table_functionwise_HO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADNewOldParaPerformanceModel zb = new FADNewOldParaPerformanceModel();
                    zb.Process = rdr["Functional_owner"].ToString();
                    zb.Total_Paras = rdr["Total_Paras"].ToString();
                    zb.Setteled_Para = rdr["Setteled_Para"].ToString();
                    zb.Unsetteled_Para = rdr["Unsetteled_Para"].ToString();
                    zb.Ratio = rdr["Ratio"].ToString();
                    zb.R1 = rdr["R1"].ToString();
                    zb.R2 = rdr["R2"].ToString();
                    zb.R3 = rdr["R3"].ToString();
                    pdetails.Add(zb);
                    }
                }
            con.Dispose();
            return pdetails;
            }

        public List<RoleActivityLogModel> GetRoleActivityLog(int ROLE_ID, int DEPT_ID, int AZ_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<RoleActivityLogModel> pdetails = new List<RoleActivityLogModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_ACTIVITY_LOG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ROLE", OracleDbType.Int32).Value = ROLE_ID;
                cmd.Parameters.Add("A_DATE", OracleDbType.Date).Value = System.DateTime.Now;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    RoleActivityLogModel zb = new RoleActivityLogModel();
                    zb.USER_NAME = rdr["E_NAME"].ToString();
                    zb.USER_PP_NUMBER = rdr["ppnum"].ToString();
                    zb.DURATION = rdr["duration"].ToString();
                    zb.ACTIONS = rdr["action"].ToString();
                    zb.ACTIVITY = rdr["GROUP_ROLE"].ToString();
                    pdetails.Add(zb);
                    }
                }
            con.Dispose();
            return pdetails;
            }

        public List<RoleActivityLogModel> GetUserActivityLog(int PP_NO)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            List<RoleActivityLogModel> pdetails = new List<RoleActivityLogModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_ACTIVITY_LOG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = PP_NO;
                cmd.Parameters.Add("ROLE", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("A_DATE", OracleDbType.Date).Value = System.DateTime.Now;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    RoleActivityLogModel zb = new RoleActivityLogModel();
                    zb.USER_NAME = rdr["E_NAME"].ToString();
                    zb.USER_PP_NUMBER = rdr["ppnum"].ToString();
                    zb.DURATION = rdr["duration"].ToString();
                    zb.START_DATE = rdr["start_time"].ToString();
                    zb.END_DATE = rdr["end_time"].ToString();
                    zb.ACTIONS = rdr["action"].ToString();
                    zb.ACTIVITY = rdr["page_id"].ToString();
                    pdetails.Add(zb);
                    }
                }
            con.Dispose();
            return pdetails;
            }

        public List<StatusWiseComplianceModel> GetStatusWiseCompliance(string AUDITEE_ID, string START_DATE, string END_DATE, string RELATION_CHECK)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<StatusWiseComplianceModel> respList = new List<StatusWiseComplianceModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_COMPLIANCE_STATUS_WISE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Varchar2).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("R_C", OracleDbType.Varchar2).Value = RELATION_CHECK;
                cmd.Parameters.Add("AUDITEE_ID", OracleDbType.Int32).Value = AUDITEE_ID;
                cmd.Parameters.Add("S_DATE", OracleDbType.Date).Value = START_DATE;
                cmd.Parameters.Add("E_DATE", OracleDbType.Date).Value = END_DATE;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    StatusWiseComplianceModel st = new StatusWiseComplianceModel();
                    st.parent_id = rdr["parent_id"].ToString();
                    st.parent_Office = rdr["parent_Office"].ToString();
                    st.entity_id = rdr["entity_id"].ToString();
                    st.entity_name = rdr["entity_name"].ToString();
                    st.auditby_id = rdr["auditby_id"].ToString();
                    st.complaince_Submitted = rdr["complaince_Submitted"].ToString();
                    st.complaince_received_at_Incharge_implementation = rdr["complaince_received_at_Incharge_implementation"].ToString();
                    st.referredback_by_Controlling_office = rdr["referredback_by_Controlling_office"].ToString();
                    st.complaince_Submitted_To_Incharge_AZ = rdr["complaince_Submitted_To_Incharge_AZ"].ToString();
                    st.complaince_Referred_back_by_Incharge_Implementation = rdr["complaince_Referred_back_by_Incharge_Implementation"].ToString();
                    st.para_settled_by_Incharge_AZ = rdr["para_settled_by_Incharge_AZ"].ToString();
                    st.complaince_Referred_back_by_Incharge_AZ = rdr["complaince_Referred_back_by_Incharge_AZ"].ToString();

                    respList.Add(st);



                    }
                }
            con.Dispose();
            return respList;

            }

        public List<AuditParaReconsillation> GetAuditParaRensillation()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditParaReconsillation> resp = new List<AuditParaReconsillation>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_FAD_audit_Para_Reconciliation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Varchar2).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditParaReconsillation rd = new AuditParaReconsillation();

                    rd.AUDIT_ZONE = rdr["Audit_zone"].ToString();
                    rd.ENTITY_TYPE_DESC = rdr["entitytypedesc"].ToString();
                    rd.REPORTING_OFFICE = rdr["Reporting_Office"].ToString();
                    rd.ENTITY_NAME = rdr["Auditee"].ToString();
                    rd.OPEN_BALANCE = rdr["Open_balance"].ToString();
                    rd.ADDED = rdr["Added"].ToString();
                    rd.TOTAL = rdr["Total"].ToString();
                    rd.SETTLED_LEGACY = rdr["Settled_Legacy"].ToString();
                    rd.SETTLED_NEW_PARA = rdr["Settled_New_Paras"].ToString();
                    rd.UN_SETTLED = rdr["Un_Settled"].ToString();
                    rd.INDICATOR = rdr["ind"].ToString();
                    rd.PERCENTAGE = rdr["percentage"].ToString();
                    rd.R1 = rdr["r1"].ToString();
                    rd.R2 = rdr["r2"].ToString();
                    rd.R3 = rdr["r3"].ToString();
                    resp.Add(rd);

                    }
                }
            con.Dispose();
            return resp;

            }

        public List<AuditPlanEngDetailReport> GetAuditPlanEngagementDetailedReport(string AUDITED_BY, string PERIOD_ID)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditPlanEngDetailReport> resp = new List<AuditPlanEngDetailReport>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_FAD_audit_Plan_Details";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("AUDIT_ID", OracleDbType.Int32).Value = AUDITED_BY;
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = PERIOD_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditPlanEngDetailReport eng = new AuditPlanEngDetailReport();
                    eng.ENG_ID = rdr["eng_id"].ToString();
                    eng.REPORTING_OFFICE = rdr["reporting_office"].ToString();
                    eng.ENTITY_NAME = rdr["name"].ToString();
                    eng.OP_START_DATE = rdr["operation_startdate"].ToString();
                    eng.OP_END_DATE = rdr["operation_enddate"].ToString();
                    eng.AUDIT_START_DATE = rdr["audit_startdate"].ToString();
                    eng.AUDIT_END_DATE = rdr["audit_enddate"].ToString();
                    eng.TRAVEL_DAYS = rdr["travel_day"].ToString();
                    eng.DISCUSSION_DAYS = rdr["discussion_day"].ToString();
                    eng.REVENUE_RECORD_DAYS = rdr["revenue_record_day"].ToString();
                    eng.WEEKEND_DAYS = rdr["total_weekend_days"].ToString();
                    eng.TOTAL_DAYS = rdr["total_days"].ToString();
                    eng.DELAY_DAYS = rdr["delay_days"].ToString();
                    eng.AUDIT_TEAM = rdr["t_name"].ToString();
                    eng.ENG_STATUS = rdr["status"].ToString();
                    resp.Add(eng);

                    }
                }
            con.Dispose();
            return resp;

            }

        public List<AnnexureExerciseStatus> GetAnnexureExerciseStatus()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AnnexureExerciseStatus> resp = new List<AnnexureExerciseStatus>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_FAD_ANNEXURE_REPORT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AnnexureExerciseStatus eng = new AnnexureExerciseStatus();
                    eng.PPNO = rdr["ppno"].ToString();
                    eng.NAME = rdr["name"].ToString();
                    eng.AUDIT_ZONE = rdr["audit_zone"].ToString();
                    eng.TOTAL = rdr["total"].ToString();
                    eng.PENDING = rdr["Pending"].ToString();
                    eng.COMPLETED = rdr["Completed"].ToString();
                    resp.Add(eng);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<GroupWiseUsersCountModel> GetGroupWiseUsersCount()
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GroupWiseUsersCountModel> resp = new List<GroupWiseUsersCountModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_GROUP_WISE_USERS_COUNT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    GroupWiseUsersCountModel m = new GroupWiseUsersCountModel();
                    m.G_ID = rdr["G_ID"].ToString();
                    m.G_NAME = rdr["G_NAME"].ToString();
                    m.U_COUNT = rdr["U_COUNT"].ToString();
                    resp.Add(m);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<GroupWisePagesModel> GetGroupWisePages(string GROUP_ID)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GroupWisePagesModel> resp = new List<GroupWisePagesModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_GROUP_WISE_PAGES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("G_ID", OracleDbType.Int32).Value = GROUP_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    GroupWisePagesModel m = new GroupWisePagesModel();
                    m.G_ID = rdr["G_ID"].ToString();
                    m.G_NAME = rdr["G_NAME"].ToString();
                    m.P_NAME = rdr["P_NAME"].ToString();
                    resp.Add(m);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<AuditeeEntitiesModel> GetEntityTypesForEntityWiseOutstandingObsPosition()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_ENTITY_TYPE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["ENTITY_TYPE"].ToString();
                    entity.CODE = Convert.ToInt32(rdr["entitycode"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<DepttWiseOutstandingParasModel> GetOutstandingParasForEntityTypeId(string ENTITY_TYPE_ID)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<DepttWiseOutstandingParasModel> resp = new List<DepttWiseOutstandingParasModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_DEPT_WISE_PARA_POSITION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("ENT_TYPE_ID", OracleDbType.Int32).Value = ENTITY_TYPE_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DepttWiseOutstandingParasModel cm = new DepttWiseOutstandingParasModel();

                    cm.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    cm.ENTITY_NAME = rdr["NAME"].ToString();
                    cm.AGE = rdr["AGE"].ToString();
                    cm.TOTAL_PARAS = rdr["TOTAL_PARAS"].ToString();
                    resp.Add(cm);

                    }
                }
            con.Dispose();
            return resp;

            }

        public List<AuditeeEntitiesModel> GetLoanStatus()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_loan_status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["description"].ToString();
                    entity.CODE = Convert.ToInt32(rdr["accountstatusid"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditeeEntitiesModel> GetLoanGLs()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_loan_gl";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["glsubname"].ToString();
                    entity.CODE = Convert.ToInt32(rdr["glsubcode"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditeeEntitiesModel> GetGMsList()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_get_gm_list";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["name"].ToString();
                    entity.CODE = Convert.ToInt32(rdr["entity_id"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<AuditeeEntitiesModel> GetRBHList(int REGION_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.R_get_rbh_list";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("gm", OracleDbType.Int32).Value = REGION_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    entity.NAME = rdr["name"].ToString();
                    entity.CODE = Convert.ToInt32(rdr["entity_id"].ToString());
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<LoanDetailReportModel> GetLoanDetailsReport(int ENT_ID, int GLSUBID, int STATUSID, DateTime START_DATE, DateTime END_DATE)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanDetailReportModel> resp = new List<LoanDetailReportModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_loan_to_default";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("g_id", OracleDbType.Int32).Value = GLSUBID;
                cmd.Parameters.Add("l_status", OracleDbType.Int32).Value = STATUSID;
                cmd.Parameters.Add("start_date", OracleDbType.Date).Value = START_DATE;
                cmd.Parameters.Add("end_date", OracleDbType.Date).Value = END_DATE;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = ENT_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanDetailReportModel pdsib = new LoanDetailReportModel();
                    pdsib.CNIC = rdr["CNIC"].ToString();
                    pdsib.LOAN_CASE_NO = rdr["LOAN_CASE_NO"].ToString();
                    pdsib.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    pdsib.GLSUBCODE = rdr["GLSUBCODE"].ToString();
                    pdsib.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    pdsib.LOAN_DISB_ID = rdr["LOAN_DISB_ID"].ToString();
                    pdsib.DISB_DATE = rdr["DISB_DATE"].ToString();
                    pdsib.LAST_TRANSACTION_DATE = rdr["LAST_TRANSACTION_DATE"].ToString();
                    pdsib.VALID_UNTIL = rdr["VALID_UNTIL"].ToString();
                    pdsib.LAST_RECOVERY_AMOUNT = rdr["LAST_RECOVERY_AMOUNT"].ToString();
                    pdsib.DISB_STATUSID = rdr["DISB_STATUSID"].ToString();
                    pdsib.PRINCIPLE = rdr["PRIN"].ToString();
                    pdsib.MARKUP = rdr["MARKUP"].ToString();
                    resp.Add(pdsib);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<LoanDetailReportModel> GetCNICLoanDetailsReport(string CNIC)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanDetailReportModel> resp = new List<LoanDetailReportModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_all_loans_of_cnic";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("C_NIC", OracleDbType.Int32).Value = CNIC;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanDetailReportModel pdsib = new LoanDetailReportModel();
                    pdsib.CNIC = rdr["CNIC"].ToString();
                    pdsib.LOAN_CASE_NO = rdr["LOAN_CASE_NO"].ToString();
                    pdsib.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    pdsib.GLSUBCODE = rdr["GLSUBCODE"].ToString();
                    pdsib.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    pdsib.LOAN_DISB_ID = rdr["LOAN_DISB_ID"].ToString();
                    pdsib.DISB_DATE = rdr["DISB_DATE"].ToString();
                    pdsib.LAST_TRANSACTION_DATE = rdr["LAST_TRANSACTION_DATE"].ToString();
                    pdsib.VALID_UNTIL = rdr["VALID_UNTIL"].ToString();
                    pdsib.LAST_RECOVERY_AMOUNT = rdr["LAST_RECOVERY_AMOUNT"].ToString();
                    pdsib.DISB_STATUSID = rdr["DISB_STATUSID"].ToString();
                    pdsib.PRINCIPLE = rdr["PRIN"].ToString();
                    pdsib.MARKUP = rdr["MARKUP"].ToString();
                    resp.Add(pdsib);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<DefaultHisotryLoanDetailReportModel> GetDefaultCNICLoanDetailsReport(string CNIC, string LOAN_DISB_ID)
            {

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<DefaultHisotryLoanDetailReportModel> resp = new List<DefaultHisotryLoanDetailReportModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_get_loan_to_default_history";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("C_NIC", OracleDbType.Int32).Value = CNIC;
                cmd.Parameters.Add("DIS_ID", OracleDbType.Varchar2).Value = LOAN_DISB_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DefaultHisotryLoanDetailReportModel pdsib = new DefaultHisotryLoanDetailReportModel();
                    pdsib.CNIC = rdr["CNIC"].ToString();
                    //pdsib.BRANCHID = rdr["BRANCHID"].ToString();
                    pdsib.NPL_LOAN_DISB_ID = rdr["npl_loan_disb_id"].ToString();
                    pdsib.DEFAULT_PRINCIPAL = rdr["default_principal"].ToString();
                    pdsib.DEFAULT_MARKUP = rdr["default_markup"].ToString();
                    pdsib.OUTSTANDING_PRINCIPAL_TOTAL = rdr["outstanding_principal_total"].ToString();
                    pdsib.OUTSTANDING_MARKUP_TOTAL = rdr["outstanding_markup_total"].ToString();
                    pdsib.CURRENT_STATUS = rdr["current_status"].ToString();
                    pdsib.TRANSACTION_DATE = rdr["transaction_date"].ToString();
                    pdsib.CNIC = rdr["cnic"].ToString();
                    pdsib.LOAN_DISB_ID = rdr["loan_disb_id"].ToString();
                    resp.Add(pdsib);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<GISTWiseReportParas> GetAuditReportParaByGistKeyword(string GIST)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GISTWiseReportParas> resp = new List<GISTWiseReportParas>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_find_gist";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("GST", OracleDbType.Varchar2).Value = GIST;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    GISTWiseReportParas mp = new GISTWiseReportParas();
                    mp.REGION = rdr["region"].ToString();
                    mp.AUDIT_ZONE = rdr["AUDIT_ZONE"].ToString();
                    mp.BRANCH = rdr["BRANCH"].ToString();
                    mp.BRANCH_CODE = rdr["BRANCH_CODE"].ToString();
                    mp.E_DATE = rdr["entereddate"].ToString();
                    mp.PARA_NO = rdr["para_no"].ToString();
                    mp.ANNEX = rdr["annex"].ToString();
                    mp.GIST_OF_PARAS = rdr["gist_of_paras"].ToString();
                    mp.NO_OF_INSTANCES = rdr["no_of_instances"].ToString();
                    mp.AMOUNT_INVOLVED = rdr["amount_involved"].ToString();
                    resp.Add(mp);
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<ComplianceProgressReportModel> GetComplianceProgressReport(string ROLE_TYPE)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ComplianceProgressReportModel> resp = new List<ComplianceProgressReportModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_COM_PROGREE_REPORT ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("R_TYPE", OracleDbType.Varchar2).Value = ROLE_TYPE;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ComplianceProgressReportModel cp = new ComplianceProgressReportModel();
                    cp.PPNO = rdr["PP_NO"].ToString();
                    cp.NAME = rdr["Name"].ToString();
                    cp.TOTAL = rdr["Total"].ToString();
                    cp.REFERRED_BACK = rdr["Referred_Back"].ToString();
                    cp.RECOMMENDED = rdr["Recommended"].ToString();
                    cp.PENDING = rdr["Pending"].ToString();
                    if (rdr["last_login"] != DBNull.Value && rdr["last_login"] != null)
                        {
                        cp.LAST_LOGIN_ON = rdr["last_login"].ToString();
                        }
                    resp.Add(cp);
                    }
                }
            con.Dispose();
            return resp;

            }

        public List<ComplianceProgressReportDetailModel> GetComplianceProgressReportDetails(string ROLE_TYPE, string PP_NO)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ComplianceProgressReportDetailModel> resp = new List<ComplianceProgressReportDetailModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_COM_PROGREE_REPORT_DETAIL ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("R_TYPE", OracleDbType.Varchar2).Value = ROLE_TYPE;
                cmd.Parameters.Add("P_NO", OracleDbType.Varchar2).Value = PP_NO;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ComplianceProgressReportDetailModel cp = new ComplianceProgressReportDetailModel();
                    cp.COMPLIANCE_UNIT = rdr["Compliance_Unit"].ToString();
                    cp.PARENT_ID = rdr["parent_id"].ToString();
                    cp.PARENT_NAME = rdr["p_name"].ToString();
                    cp.ENTITY_ID = rdr["entity_id"].ToString();
                    cp.ENTITY_NAME = rdr["name"].ToString();
                    cp.ENTITY_CODE = rdr["code"].ToString();
                    cp.COM_KEY = rdr["COM_KEY"].ToString();
                    cp.PP_NO = rdr["PP_NO"].ToString();
                    cp.EMP_NAME = rdr["emp_name"].ToString();
                    cp.TOTAL = rdr["Total"].ToString();
                    cp.REFERRED_BACK = rdr["Refered_back"].ToString();
                    cp.RECOMMENDED = rdr["Satisfied"].ToString();
                    cp.PENDING = rdr["Pending"].ToString();

                    resp.Add(cp);
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<AuditEntitiesModel> GetEntityTypesForSettlementReport()
            {

            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditEntitiesModel> entitiesList = new List<AuditEntitiesModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_ENTITY_TYPE_FOR_SETTLEMENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditEntitiesModel entity = new AuditEntitiesModel();
                    entity.AUTID = Convert.ToInt32(rdr["autid"]);
                    entity.ENTITYTYPEDESC = rdr["entitytypedesc"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public List<SettledParasModel> GetSettledParasForComplianceReport(int ENTITY_TYPE_ID, DateTime? DATE_FROM, DateTime? DATE_TO)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<SettledParasModel> resp = new List<SettledParasModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_COMPLIANCE_REPORT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("S_ENT_ID", OracleDbType.Int32).Value = ENTITY_TYPE_ID;
                cmd.Parameters.Add("S_DATE_FROM", OracleDbType.Date).Value = DATE_FROM;
                cmd.Parameters.Add("S_DATE_TO", OracleDbType.Date).Value = DATE_TO;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    SettledParasModel cp = new SettledParasModel();
                    cp.ENTITY_ID = rdr["entity_id"].ToString();
                    cp.PARENT_ID = rdr["parent_id"].ToString();
                    cp.REPORTING_OFFICE = rdr["p_name"].ToString();
                    cp.PLACE_OF_POSTING = rdr["c_name"].ToString();
                    cp.AUDIT_PERIOD = rdr["period"].ToString();
                    cp.PARA_NO = rdr["para_no"].ToString();
                    cp.GIST = rdr["Gist"].ToString();
                    cp.SETTLED_ON = rdr["stelled_on"].ToString();
                    cp.AUDITED_BY = rdr["auditedby"].ToString();
                    resp.Add(cp);
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<ComplianceOSParasModel> GetParasForComplianceSummaryReport()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ComplianceOSParasModel> resp = new List<ComplianceOSParasModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_PARA_POSITION_SUM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ComplianceOSParasModel cp = new ComplianceOSParasModel();
                    cp.ENTITY_ID = rdr["entity_id"].ToString();
                    cp.PARENT_ID = rdr["parent_id"].ToString();
                    cp.ENTITY_NAME = rdr["ent_name"].ToString();
                    cp.ENTITY_TYPE = rdr["ent_type"].ToString();
                    cp.REPORTING_OFFICE = rdr["p_name"].ToString();
                    cp.TOTAL_PARAS = rdr["t_paras"].ToString();
                    cp.TOTAL_SETTLED_PARAS = rdr["t_s_paras"].ToString();
                    cp.TOTAL_OUTSTANDING_PARAS = rdr["t_os_paras"].ToString();

                    cp.COMPLIANCE_PENDING_OS_PARAS = "0";//rdr["cos_paras"].ToString();
                    cp.ZERO_COMPLIANCE_PARAS = rdr["z_cos_paras"].ToString();
                    resp.Add(cp);
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<EngPlanDelayAnalysisReportModel> GetEngagementPlanDelayAnalysisReport()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<EngPlanDelayAnalysisReportModel> resp = new List<EngPlanDelayAnalysisReportModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.p_delay_audits";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    EngPlanDelayAnalysisReportModel cp = new EngPlanDelayAnalysisReportModel();
                    cp.ENTITY_ID = rdr["entity_id"].ToString();
                    cp.REPORTING_OFFICE = rdr["p_name"].ToString();
                    cp.PLACE_OF_POSTING = rdr["c_name"].ToString();
                    cp.ENTITY_NAME = rdr["name"].ToString();
                    cp.AUDIT_START_DATE = rdr["audit_startdate"].ToString();
                    cp.AUDIT_END_DATE = rdr["audit_enddate"].ToString();
                    cp.STATUS = rdr["status"].ToString();
                    cp.DELAY_DAYS = rdr["no_of_days"].ToString();
                    resp.Add(cp);
                    }
                }
            con.Dispose();
            return resp;
            }

        //------------------- FAD Monthly Review Report 
        public List<FADMonthlyReviewParasModel> GetFADMonthlyReviewParasForEntityTypeId(string ENT_TYPE_ID, DateTime? S_DATE, DateTime? E_DATE)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var list = new List<FADMonthlyReviewParasModel>();
            try
                {
                using (var con = this.DatabaseConnection())
                    {
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        {
                        cmd.CommandText = "pkg_rpt.R_FAD_MONTHLY_REVIEW";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("R_T", OracleDbType.Int32).Value = ENT_TYPE_ID;
                        cmd.Parameters.Add("S_DATE", OracleDbType.Date).Value = S_DATE;
                        cmd.Parameters.Add("E_DATE", OracleDbType.Date).Value = E_DATE;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                var review = new FADMonthlyReviewParasModel
                                    {
                                    REPORTING_OFFICE = rdr["P_NAME"].ToString(),
                                    PLACE_OF_POSTING = rdr["C_NAME"].ToString(),
                                    CHILD_CODE = rdr["CHILD_CODE"].ToString(),
                                    OPENING_BALANCE = rdr["opening_bal"].ToString(),
                                    PARA_ADDED = rdr["Para_added"].ToString(),
                                    SETTLED_COM = rdr["Settled_com"].ToString(),
                                    SETTLED_AUDIT = rdr["Settled_aud"].ToString(),
                                    OUTSTANDING = rdr["Outstanding"].ToString(),
                                    R1 = rdr["r1"].ToString(),
                                    R2 = rdr["r2"].ToString(),
                                    R3 = rdr["r3"].ToString()
                                    };


                                list.Add(review);
                                }
                            }
                        }
                    }
                }
            catch (Exception)
                {

                throw;
                }

            return list;
            }

        public List<SeriousFraudulentObsGM> GetSeriousFraudulentObsGMOverview()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<SeriousFraudulentObsGM> list = new List<SeriousFraudulentObsGM>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_GM_WISE_SERIOUS_PARAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("R_T", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("S_DATE", OracleDbType.Date).Value = new DateTime();
                cmd.Parameters.Add("E_DATE", OracleDbType.Date).Value = new DateTime();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    SeriousFraudulentObsGM chk = new SeriousFraudulentObsGM();
                    chk.PARENT_ID = Convert.ToInt32(rdr["PARENT_ID"].ToString());
                    chk.P_NAME = rdr["P_NAME"].ToString();
                    chk.TOTAL_NO = rdr["total_before_current_year"].ToString();
                    chk.C_TOTAL_NO = rdr["total_in_current_year"].ToString(); // Total no of Serious Observation in CURRENT
                    chk.A1 = rdr["a1_before_current_year"].ToString();
                    chk.C_A1 = rdr["a1_in_current_year"].ToString();
                    chk.AMOUNT = rdr["c_amount_before_current_year"].ToString();
                    chk.C_AMOUNT = rdr["c_amount_in_current_year"].ToString();
                    chk.PER_INV = rdr["PER_INV"].ToString();
                    chk.C_PER_INV = rdr["C_PER_INV"].ToString();

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<SeriousFraudulentObsGMDetails> GetSeriousFraudulentObsGMDetails(string INDICATOR, int PARENT_ENT_ID, string ANNEX_IND)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<SeriousFraudulentObsGMDetails> list = new List<SeriousFraudulentObsGMDetails>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_GM_WISE_SERIOUS_PARAS_DETAILS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("PARENT_ENT_ID", OracleDbType.Int32).Value = PARENT_ENT_ID;
                cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("P_ANNEX", OracleDbType.Varchar2).Value = ANNEX_IND;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;

                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    SeriousFraudulentObsGMDetails chk = new SeriousFraudulentObsGMDetails();
                    chk.P_NAME = rdr["p_name"].ToString();
                    chk.C_NAME = rdr["c_name"].ToString();
                    chk.AUDIT_PERIOD = rdr["audit_period"].ToString();
                    chk.PARA_NO = rdr["para_no"].ToString();
                    chk.ANNEX_HEADING = rdr["heading"].ToString();
                    chk.RISK = rdr["risk"].ToString();
                    chk.GIST_OF_PARAS = rdr["gist_of_paras"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["amount_involved"].ToString();

                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<YearWiseOutstandingObservationsModel> GetYearWiseOutstandingParas(int ENTITY_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<YearWiseOutstandingObservationsModel> responseList = new List<YearWiseOutstandingObservationsModel>();
            var loggedInUser = sessionHandler.GetSessionUser();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_YEAR_WISE_AUDIT_OUTSTANDING_PARAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        YearWiseOutstandingObservationsModel record = new YearWiseOutstandingObservationsModel()
                            {
                            AUDIT_PERIOD = rdr["audit_period"].ToString(),
                            ENTITY_ID = rdr["entity_id"].ToString(),
                            TOTAL_PARAS = rdr["Total_Paras"].ToString(),
                            SETTLED_PARA = rdr["Settled_para"].ToString(),
                            UN_SETTLED_PARA = rdr["Un_Settled_para"].ToString(),
                            R1 = rdr["R1"].ToString(),
                            R2 = rdr["R2"].ToString(),
                            R3 = rdr["R3"].ToString()
                            };
                        responseList.Add(record);
                        }
                    }
                }
            con.Dispose();
            return responseList;
            }

        public List<AuditeeOldParasModel> GetYearWiseOutstandingParasDetails(int ENTITY_ID = 0, int AUDIT_PERIOD = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_YEAR_WISE_AUDIT_OUTSTANDING_PARAS_DETAILS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("A_PERIOD", OracleDbType.Int32).Value = AUDIT_PERIOD;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    AuditeeOldParasModel chk = new AuditeeOldParasModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.MEMO_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.OBS_ID = rdr["OBS_ID"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<ParaTextSearchModel> GetAuditParasByText(string SEARCH_KEYWORD)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ParaTextSearchModel> list = new List<ParaTextSearchModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_PARA_TEXT_WORDS_V2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_TEXT", OracleDbType.Varchar2).Value = SEARCH_KEYWORD;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ParaTextSearchModel chk = new ParaTextSearchModel
                        {
                        AUDIT_ZONE = rdr["name"]?.ToString(),
                        PARENT_NAME = rdr["p_name"]?.ToString(),
                        CHILD_NAME = rdr["c_name"]?.ToString(),
                        AUDIT_PERIOD = rdr["audit_period"]?.ToString(),
                        ANNEXURE = rdr["annex"]?.ToString(),
                        PARA_NO = rdr["para_no"]?.ToString(),
                        GIST_OF_PARAS = rdr["gist_of_paras"]?.ToString(),
                        TEXT = rdr["text"]?.ToString()

                        };

                    list.Add(chk);
                    }


                }
            con.Dispose();
            return list;
            }

        public List<YearWiseAllParasModel> GetYearWiseAllParas(string A_PERIOD)
            {

            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<YearWiseAllParasModel> entitiesList = new List<YearWiseAllParasModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_rpt.P_GET_YEAR_WISE_ALL_PARAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("A_PERIOD", OracleDbType.Int32).Value = A_PERIOD;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    YearWiseAllParasModel entity = new YearWiseAllParasModel
                        {
                        AUDIT_PERIOD = rdr["audit_period"]?.ToString(),
                        ENTITY_TYPE = rdr["ENTITY_TYPE"]?.ToString(),
                        REPORTING_OFFICE = rdr["reporting_office"]?.ToString(),
                        ENTITY_NAME = rdr["entity_name"]?.ToString(),
                        ENTITY_RISK_LEVEL = rdr["entity_risk"]?.ToString(),
                        FUNCTION_RESP = rdr["func_resp"]?.ToString(),
                        AUDIT_ZONE = rdr["Audit_Zone"]?.ToString(),
                        PARA_NO = rdr["para_no"]?.ToString(),
                        GIST_OF_PARAS = rdr["gist_of_paras"]?.ToString(),
                        RISK = rdr["RISK"]?.ToString(),
                        ANNEXURE = rdr["annexure"]?.ToString(),
                        AMOUNT_INVOLVED = rdr["amount_involved"]?.ToString(),
                        NO_OF_INSTANCES = rdr["no_of_instances"]?.ToString(),
                        PARA_STATUS = rdr["PARA_STATUS"]?.ToString()
                        };
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        }
    }

