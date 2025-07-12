using AIS.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace AIS.Controllers
{
    public partial class DBConnection
    {
        public List<AuditEmployeeModel> GetFadAuditEmployees(int entityId)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<AuditEmployeeModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetAuditEmployees";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_entity_id", OracleDbType.Int32).Value = entityId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var m = new AuditEmployeeModel();
                            m.PPNO = rdr["PPNO"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["PPNO"]);
                            m.DEPARTMENTCODE = rdr["DEPARTMENTCODE"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DEPARTMENTCODE"]);
                            m.DESIGNATIONCODE = rdr["DESIGNATIONCODE"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DESIGNATIONCODE"]);
                            m.RANKCODE = rdr["RANKCODE"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RANKCODE"]);
                            m.DEPTARMENT = rdr["DEPTARMENT"].ToString();
                            m.EMPLOYEEFIRSTNAME = rdr["EMPLOYEEFIRSTNAME"].ToString();
                            m.EMPLOYEELASTNAME = rdr["EMPLOYEELASTNAME"].ToString();
                            m.CURRENT_RANK = rdr["CURRENT_RANK"].ToString();
                            m.FUN_DESIGNATION = rdr["FUN_DESIGNATION"].ToString();
                            m.TYPE = rdr["TYPE"].ToString();
                            list.Add(m);
                        }
                    }
                }
            con.Close();
            return list;
        }

        public List<IdNameModel> GetRelationTypes()
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<IdNameModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetRelationTypes";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new IdNameModel
                            {
                                Id = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                                Name = rdr["NAME"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }

        public List<IdNameModel> GetReportingOffices(int relationTypeId)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<IdNameModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetReportingOffices";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_relation_id", OracleDbType.Int32).Value = relationTypeId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new IdNameModel
                            {
                                Id = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                                Name = rdr["NAME"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }

        public List<EntityModel> GetEntitiesForOffice(int reportingOfficeId)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<EntityModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetEntitiesForOffice";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_office_id", OracleDbType.Int32).Value = reportingOfficeId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new EntityModel
                            {
                                EntityId = rdr["ENTITY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ENTITY_ID"]),
                                Name = rdr["NAME"].ToString(),
                                Type = rdr["TYPE"]?.ToString(),
                                TotalParas = rdr["TOTAL_PARAS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TOTAL_PARAS"])
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }

        public string AllocateEntityToAuditor(int azId, int entId, int auditorPPNO)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            string resp = string.Empty;
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_allocate_entity_to_auditor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_az_id", OracleDbType.Int32).Value = azId;
                    cmd.Parameters.Add("p_ent_id", OracleDbType.Int32).Value = entId;
                    cmd.Parameters.Add("p_auditor_ppno", OracleDbType.Int32).Value = auditorPPNO;
                    //cmd.Parameters.Add("p_assigned_by", OracleDbType.Int32).Value = assignedBy;
                    cmd.Parameters.Add("io_msg", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    resp = cmd.Parameters["io_msg"].Value?.ToString();
                }
            con.Close();
            return resp;
        }

        public List<ObservationReferenceModel> GetObservationsForReferenceUpdate(int? entId, int? assignedAuditorId, int? referenceId)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<ObservationReferenceModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetObservationsForReferenceUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_ent_id", OracleDbType.Int32).Value = entId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_auditor", OracleDbType.Int32).Value = assignedAuditorId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_ref_id", OracleDbType.Int32).Value = referenceId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new ObservationReferenceModel
                            {
                                ComId = rdr["COM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["COM_ID"]),
                                EntId = rdr["ENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ENT_ID"]),
                                ParaTitle = rdr["PARA_TITLE"].ToString(),
                                ReferenceId = rdr["REFERENCE_ID"] == DBNull.Value ? null : (int?)Convert.ToInt32(rdr["REFERENCE_ID"]),
                                ReferenceType = rdr["REFERENCE_TYPE"].ToString(),
                                AssignedAuditorId = rdr["ASSIGNED_AUDITOR"] == DBNull.Value ? null : (int?)Convert.ToInt32(rdr["ASSIGNED_AUDITOR"]),
                                Status = rdr["STATUS"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }

        public string UpdateParaReference(int comId, int newRef)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            string resp = string.Empty;
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_update_para_reference";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_com_id", OracleDbType.Int32).Value = comId;
                    cmd.Parameters.Add("p_new_ref", OracleDbType.Int32).Value = newRef;
                    //cmd.Parameters.Add("p_updated_by", OracleDbType.Int32).Value = updatedBy;
                    cmd.Parameters.Add("io_msg", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    resp = cmd.Parameters["io_msg"].Value?.ToString();
                }
            con.Close();
            return resp;
        }

        public List<UpdateLogModel> GetUpdateLog(int comId)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<UpdateLogModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_get_update_log";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_com_id", OracleDbType.Int32).Value = comId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new UpdateLogModel
                            {
                                Date = rdr["ACTION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["ACTION_DATE"]),
                                User = rdr["ACTION_USER"].ToString(),
                                Field = rdr["ACTION_FIELD"].ToString(),
                                OldValue = rdr["OLD_VALUE"].ToString(),
                                NewValue = rdr["NEW_VALUE"].ToString(),
                                ActionType = rdr["ACTION_TYPE"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }

        public List<ReferenceSearchResultModel> SearchReferences(string referenceType, string keyword)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();

            var list = new List<ReferenceSearchResultModel>();
            using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_SearchReferences";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_ref_type", OracleDbType.Varchar2).Value = referenceType;
                    cmd.Parameters.Add("p_keyword", OracleDbType.Varchar2).Value = keyword;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new ReferenceSearchResultModel
                            {
                                ReferenceId = rdr["REFERENCE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["REFERENCE_ID"]),
                                Title = rdr["TITLE"].ToString(),
                                ReferenceType = rdr["REFERENCE_TYPE"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
        }
    }
}
