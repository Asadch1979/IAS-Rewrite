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

        public string SaveCircularDocument(CircularDocumentModel model)
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            using (var con = this.DatabaseConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_InsertCircularDoc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_circular_id", OracleDbType.Int32).Value = model.CircularId;
                    cmd.Parameters.Add("p_file_name", OracleDbType.Varchar2).Value = model.FileName;
                    cmd.Parameters.Add("p_file_type", OracleDbType.Varchar2).Value = model.FileType;
                    cmd.Parameters.Add("p_file_size", OracleDbType.Int32).Value = model.FileSize;
                    cmd.Parameters.Add("p_file_blob", OracleDbType.Blob).Value = model.FileBlob;
                    cmd.Parameters.Add("p_uploaded_by", OracleDbType.Varchar2).Value = model.UploadedBy;
                    cmd.Parameters.Add("o_status", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["o_status"].Value?.ToString();
                }
            }
        }

        public void InsertCircularDoc(
            int circularId,
            string fileName,
            string fileType,
            long fileSize,
            byte[] fileBlob,
            string uploadedBy,
            out string status)
        {
            using (var con = this.DatabaseConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_InsertCircularDoc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_circular_id", OracleDbType.Int32).Value = circularId;
                    cmd.Parameters.Add("p_file_name", OracleDbType.Varchar2).Value = fileName;
                    cmd.Parameters.Add("p_file_type", OracleDbType.Varchar2).Value = fileType;
                    cmd.Parameters.Add("p_file_size", OracleDbType.Int32).Value = fileSize;
                    cmd.Parameters.Add("p_file_blob", OracleDbType.Blob).Value = fileBlob;
                    cmd.Parameters.Add("p_uploaded_by", OracleDbType.Varchar2).Value = uploadedBy;
                    cmd.Parameters.Add("o_status", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    status = cmd.Parameters["o_status"].Value?.ToString();
                }
            }
        }

        public CircularDocumentModel GetCircularDocument(int docId)
        {
            using (var con = this.DatabaseConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetCircularDoc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_doc_id", OracleDbType.Int32).Value = docId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new CircularDocumentModel
                            {
                                DocId = docId,
                                CircularId = rdr["CIRCULAR_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CIRCULAR_ID"]),
                                FileName = rdr["FILE_NAME"].ToString(),
                                FileType = rdr["FILE_TYPE"].ToString(),
                                FileSize = rdr["FILE_SIZE"] == DBNull.Value ? 0 : Convert.ToInt64(rdr["FILE_SIZE"]),
                                FileBlob = rdr["FILE_BLOB"] == DBNull.Value ? null : ((OracleBlob)rdr.GetOracleBlob(rdr.GetOrdinal("FILE_BLOB"))).Value,
                                UploadedBy = rdr["UPLOADED_BY"].ToString(),
                                UploadedOn = rdr["UPLOADED_ON"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["UPLOADED_ON"])
                            };
                        }
                    }
                }
            }
            return null;
        }
            


        public List<AuditChecklistAnnexureCircularModel> GetAuditChecklistAnnexureCirculars()
            {
           
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var list = new List<AuditChecklistAnnexureCircularModel>();
            var con = this.DatabaseConnection();
            con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_GetAuditChecklistAnnexureCirculars";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var rdr = cmd.ExecuteReader())
                        {
                        while (rdr.Read())
                            {
                            list.Add(new AuditChecklistAnnexureCircularModel
                                {
                                ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                                DivisionEntId = rdr["DIVISION_ENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DIVISION_ENT_ID"]),
                                ReferenceTypeId = rdr["REFERENCE_TYPE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["REFERENCE_TYPE_ID"]),
                                ReferenceType = rdr["REFERENCE_TYPE"]?.ToString(),
                                InstructionsDetails = rdr["INSTRUCTIONSDETAILS"]?.ToString(),
                                Keywords = rdr["KEYWORDS"]?.ToString(),
                                RedirectedPage = rdr["REDIRECTEDPAGE"]?.ToString(),
                                Division = rdr["DIVISION"]?.ToString(),
                                InstructionsTitle = rdr["INSTRUCTIONSTITLE"]?.ToString(),
                                InstructionsDate = rdr["INSTRUCTIONSDATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["INSTRUCTIONSDATE"]),
                                DocType = rdr["DOCTYPE"]?.ToString()
                                });
                            }
                        }
                    
                }
            return list;
            }


        public List<AuditEmployeeModel> GetFadAuditEmployees()
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
                    cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
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
                            m.TASK_ALLOCATED = rdr["TASK_ALLOCATED"] == DBNull.Value ? string.Empty : rdr["TASK_ALLOCATED"].ToString();
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
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
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
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
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

        public List<EntityModel> Get_Entities_For_Office(int reportingOfficeId)
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
                                EntityCode = rdr["ENTITY_CODE"]?.ToString(),
                                Name = rdr["NAME"].ToString(),
                                Type = rdr["TYPE"]?.ToString(),
                                Allocatedto = rdr["ALLOCATEDTO"]?.ToString(),
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
            var con = this.DatabaseConnection(); con.Open();
            string result = "";
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_fad.P_allocate_entity_to_auditor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_az_id", OracleDbType.Int32).Value = azId;
                cmd.Parameters.Add("p_ent_id", OracleDbType.Int32).Value = entId;
                cmd.Parameters.Add("p_auditor_ppno", OracleDbType.Int32).Value = auditorPPNO;
                cmd.Parameters.Add("p_assigned_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    if (rdr.Read())
                        {
                        result = rdr["remarks"].ToString();
                        }
                    }
                }
            con.Dispose();
            return result;
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
                cmd.Parameters.Add("p_updated_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (var rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        resp = rdr["remarks"]?.ToString();
                        }
                    }
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
                cmd.Parameters.Add("p_ref_type", OracleDbType.Varchar2).Value = referenceType ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_keyword", OracleDbType.Varchar2).Value = keyword ?? (object)DBNull.Value;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (var rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        list.Add(new ReferenceSearchResultModel
                            {
                            ReferenceId = rdr["REFERENCE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["REFERENCE_ID"]),
                            Title = rdr["TITLE"] == DBNull.Value ? "" : rdr["TITLE"].ToString(),
                            ReferenceType = rdr["REFERENCE_TYPE"] == DBNull.Value ? "" : rdr["REFERENCE_TYPE"].ToString(),
                            INSTRUCTIONSDETAILS = rdr["INSTRUCTIONSDETAILS"] == DBNull.Value ? "" : rdr["INSTRUCTIONSDETAILS"].ToString(),
                            KEYWORDS = rdr["KEYWORDS"] == DBNull.Value ? "" : rdr["KEYWORDS"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
            }


        public List<PendingParaModel> GetPendingParas(int entityId, int auditYear)
        {
            var list = new List<PendingParaModel>();
            using (var con = this.DatabaseConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetPendingParas";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_entity_id", OracleDbType.Int32).Value = entityId;
                    cmd.Parameters.Add("p_audit_year", OracleDbType.Int32).Value = auditYear;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new PendingParaModel
                                {
                                ParaId = Convert.ToInt32(rdr["PARA_ID"]),
                                AuditYear = rdr["AUDIT_YEAR"].ToString(),
                                ParaNo = rdr["PARA_NO"].ToString(),
                                Gist = rdr["GIST"].ToString(),
                                Risk = rdr["RISK"].ToString()
                                });
                        }
                    }
                }
            }
            return list;
        }

        public List<EntityTaskSummaryModel> GetEntityTaskSummary()
        {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var list = new List<EntityTaskSummaryModel>();
            using (var con = this.DatabaseConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "PKG_FAD.P_GetEntityTaskSummary";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_auditor_ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new EntityTaskSummaryModel
                            {
                                EntityId = Convert.ToInt32(rdr["ENTITY_ID"]),
                                EntityCode = rdr["ENTITY_CODE"].ToString(),
                                EntityName = rdr["ENTITY_NAME"].ToString(),
                                AuditYear = rdr["AUDIT_YEAR"].ToString(),
                                TotalParas = Convert.ToInt32(rdr["TOTAL_PARAS"]),
                                ParasUpdated = Convert.ToInt32(rdr["PARAS_UPDATED"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
