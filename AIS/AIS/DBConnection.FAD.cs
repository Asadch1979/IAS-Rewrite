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
                    var status = cmd.Parameters["o_status"].Value?.ToString();
                    return status;
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

        public string AllocateEntityToAuditor(int azId, int entId, int auditorPpno)
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
                cmd.Parameters.Add("p_auditor_ppno", OracleDbType.Int32).Value = auditorPpno;
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
                            ParaTitle = rdr["PARA_TITLE"].ToString(),                            ReferenceType = rdr["REFERENCE_TYPE"].ToString(),
                            AssignedAuditorId = rdr["ASSIGNED_AUDITOR"] == DBNull.Value ? null : (int?)Convert.ToInt32(rdr["ASSIGNED_AUDITOR"]),
                            Status = rdr["STATUS"].ToString()
                            });
                        }
                    }
                }
            con.Close();
            return list;
            }

        public string UpdateParaReference(int comId, int? linkId, int newRef)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            // Call the unified procedure with UPDATE action
            var result = ManageReference(
                "UPDATE",
                new ParaReferenceLinkModel { LinkId = linkId },
                comId,
                null,
                newRef,
                loggedInUser.PPNumber);
            return result.remarks;
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
                cmd.CommandText = "PKG_FAD.P_GetReferenceUpdateLog";
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
                            Title = rdr["TITLE"] == DBNull.Value ? "" : rdr["TITLE"].ToString(),
                            InstructionsDate = rdr["INSTRUCTIONSDATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["INSTRUCTIONSDATE"]),
                            ReferenceType = rdr["REFERENCE_TYPE"] == DBNull.Value ? "" : rdr["REFERENCE_TYPE"].ToString(),
                            INSTRUCTIONSDETAILS = rdr["INSTRUCTIONSDETAILS"] == DBNull.Value ? "" : rdr["INSTRUCTIONSDETAILS"].ToString(),
                            KEYWORDS = rdr["KEYWORDS"] == DBNull.Value ? "" : rdr["KEYWORDS"].ToString(),
                            REFERENCEURL = rdr["REFERENCEURL"] == DBNull.Value ? "" : rdr["REFERENCEURL"].ToString()
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

        public List<ReferenceEntitySummaryModel> GetReferenceEntitySummary()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var list = new List<ReferenceEntitySummaryModel>();
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
                            list.Add(new ReferenceEntitySummaryModel
                                {
                                EntityId = Convert.ToInt32(rdr["ENTITY_ID"]),
                                EntityCode = rdr["ENTITY_CODE"].ToString(),
                                EntityName = rdr["ENTITY_NAME"].ToString(),
                                AuditPeriod = rdr["AUDIT_PERIOD"].ToString(),
                                TotalParas = Convert.ToInt32(rdr["TOTAL_PARAS"]),
                                UpdatedParas = Convert.ToInt32(rdr["UPDATED_PARAS"]),
                                Pendency = Convert.ToInt32(rdr["PENDENCY"])
                                });
                            }
                        }
                    }
                }
            return list;
            }

        public List<PendingReferenceParaModel> GetPendingReferenceParas()
            {
            var list = new List<PendingReferenceParaModel>();
            using (var con = this.DatabaseConnection())
                {
                con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_GetPendingReferenceParas";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                        {
                        while (rdr.Read())
                            {
                            list.Add(new PendingReferenceParaModel
                                {
                                ComId = rdr["COM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["COM_ID"]),
                                AuditPeriod = rdr["AUDIT_PERIOD"]?.ToString(),
                                ParaNo = rdr["PARA_NO"]?.ToString(),
                                GistOfParas = rdr["GIST_OF_PARAS"]?.ToString()
                                });
                            }
                        }
                    }
                }
            return list;
            }

        public ParaReferenceDataModel GetParaReferenceData(int comId)
            {
            var model = new ParaReferenceDataModel
                {
                References = new List<int>(),
                ReferenceDetails = new List<AuditChecklistAnnexureCircularModel>(),
                ReferenceLinks = new List<ParaReferenceLinkModel>()
                };

            model.ParaText = GetParaText(comId);

            var links = GetParaReferenceLinks(comId);
            model.ReferenceLinks = links;
            model.References = links.Select(l => l.ReferenceId).ToList();

            var allRefs = GetAuditChecklistAnnexureCirculars();
            if (model.References != null && model.References.Count > 0)
                {
                model.ReferenceDetails = allRefs.Where(r => model.References.Contains(r.ID)).ToList();
                foreach (var det in model.ReferenceDetails)
                    {
                    var lnk = links.FirstOrDefault(l => l.ReferenceId == det.ID);
                    if (lnk != null)
                        det.LinkId = lnk.LinkId;
                    }
                }

            return model;
            }

        public List<ParaReferenceLinkModel> GetParaReferenceLinks(int comId)
            {
            var list = new List<ParaReferenceLinkModel>();
            using (var con = this.DatabaseConnection())
                {
                con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_GetParaReferences";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_com_id", OracleDbType.Int32).Value = comId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                        {
                        while (rdr.Read())
                            {
                            list.Add(new ParaReferenceLinkModel
                                {
                                LinkId = rdr["LINK_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["LINK_ID"]),
                                EntityId = rdr["ENTITY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ENTITY_ID"]),
                                OldParaId = rdr["OLD_PARA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["OLD_PARA_ID"]),
                                NewParaId = rdr["NEW_PARA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["NEW_PARA_ID"]),
                                ParaId = rdr["PARA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["PARA_ID"]),
                                ReferenceId = rdr["REFERENCE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["REFERENCE_ID"]),
                                ReferenceTitle = rdr["REFERENCE_TITLE"].ToString(),
                                CreditManualId = rdr["CREDIT_MANUAL_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["CREDIT_MANUAL_ID"]),
                                OpManualId = rdr["OP_MANUAL_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["OP_MANUAL_ID"]),
                                ManualType = rdr["MANUAL_TYPE"].ToString(),
                                Chapter = rdr["CHAPTER"].ToString(),
                                MatchedText = rdr["MATCHED_TEXT"].ToString(),
                                LinkType = rdr["LINK_TYPE"].ToString()
                                });
                            }
                        }
                    }
                }
            return list;
            }

        public List<int> GetParaReferences(int comId)
            {
            var list = new List<int>();
            using (var con = this.DatabaseConnection())
                {
                con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_GetParaReferences";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_com_id", OracleDbType.Int32).Value = comId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                        {
                        while (rdr.Read())
                            {
                            list.Add(rdr["REFERENCE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["REFERENCE_ID"]));
                            }
                        }
                    }
                }
            return list;
            }

        public string GetParaText(int comId)
            {
            string text = string.Empty;
            using (var con = this.DatabaseConnection())
                {
                con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_GetParaText";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_com_id", OracleDbType.Int32).Value = comId;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (var rdr = cmd.ExecuteReader())
                        {
                        if (rdr.Read())
                            text = rdr["PARA_TEXT"]?.ToString();
                        }
                    }
                }
            return text;
            }

        public AuditChecklistAnnexureCircularModel GetReferenceDetail(int refId)
            {
            return GetAuditChecklistAnnexureCirculars().FirstOrDefault(r => r.ID == refId);
            }

        public List<AuditChecklistAnnexureCircularModel> GetReferenceDetails(List<int> ids)
            {
            var all = GetAuditChecklistAnnexureCirculars();
            return all.Where(r => ids.Contains(r.ID)).ToList();
            }

        /// <summary>
        /// Centralized method to execute <c>PKG_FAD.P_ManageReference</c>. It
        /// accepts the action to perform and relevant parameters. Unused
        /// parameters for a given action may be <c>null</c>.
        /// </summary>
        private (string remarks, string action, int? paraId) ManageReference(
            string action,
            ParaReferenceLinkModel link,
            int? paraId,
            int? refId,
            int? newRef,
            string ppno)
            {
            using (var con = this.DatabaseConnection())
                {
                con.Open();
                using (var cmd = con.CreateCommand())
                    {
                    cmd.CommandText = "PKG_FAD.P_ManageReference";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_action", OracleDbType.Varchar2).Value = action;
                    cmd.Parameters.Add("p_link_id", OracleDbType.Int32).Value = link?.LinkId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_entity_id", OracleDbType.Int32).Value = link?.EntityId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_old_para_id", OracleDbType.Int32).Value = link?.OldParaId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_new_para_id", OracleDbType.Int32).Value = link?.NewParaId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_para_id", OracleDbType.Int32).Value = paraId ?? link?.ParaId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_ref_id", OracleDbType.Int32).Value = refId ?? link?.ReferenceId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_ref_title", OracleDbType.Varchar2).Value = link?.ReferenceTitle ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_credit_manual_id", OracleDbType.Int32).Value = link?.CreditManualId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_op_manual_id", OracleDbType.Int32).Value = link?.OpManualId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_manual_type", OracleDbType.Varchar2).Value = link?.ManualType ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_chapter", OracleDbType.Varchar2).Value = link?.Chapter ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_matched_text", OracleDbType.Varchar2).Value = link?.MatchedText ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_link_type", OracleDbType.Varchar2).Value = link?.LinkType ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_new_ref", OracleDbType.Int32).Value = newRef ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_user", OracleDbType.Varchar2).Value = ppno;
                    cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var reader = cmd.ExecuteReader())
                        {
                        if (reader.Read())
                            {
                            var remarks = reader["remarks"]?.ToString();
                            var returnedAction = reader["action"]?.ToString();
                            int? returnedParaId = null;
                            if (reader["para_id"] != DBNull.Value)
                                returnedParaId = Convert.ToInt32(reader["para_id"]);

                            return (remarks, returnedAction, returnedParaId);
                            }
                        }

                    return (null, null, null);
                    }
                }
            }

        /// <summary>
        /// Wrapper for managing para references. This now calls the unified
        /// <c>PKG_FAD.P_ManageReference</c> procedure with the <c>ADD</c> action
        /// instead of the legacy <c>P_AddReference</c> procedure.
        /// </summary>
        private string AddReference(ParaReferenceLinkModel link, string ppno)
            {
            var resp = ManageReference(
                "ADD",
                link,
                link?.ParaId,
                link?.ReferenceId,
                null,
                ppno);
            return resp.remarks;
            }

        /// <summary>
        /// Removes a para reference using <c>P_ManageReference</c> with the
        /// <c>DELETE</c> action.
        /// </summary>
        private string DeleteReference(int comId, int? linkId, int refId, string ppno)
            {
            var resp = ManageReference(
                "DELETE",
                new ParaReferenceLinkModel { LinkId = linkId },
                comId,
                refId,
                null,
                ppno);
            return resp.remarks;
            }

        // reference_reviewed flag is now updated inside P_ManageReference so
        // the standalone MarkParaAsReviewed method is no longer required.

        public string SaveParaReferences(int comId, List<ParaReferenceLinkModel> references)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var user = sessionHandler.GetSessionUser();

            var existing = GetParaReferenceLinks(comId);
            string result = string.Empty;

            foreach (var oldRef in existing)
                {
                if (!references.Any(r => r.LinkId == oldRef.LinkId))
                    {
                    result = DeleteReference(comId, oldRef.LinkId, oldRef.ReferenceId, user.PPNumber);
                    }
                }

            foreach (var r in references)
                {
                var match = existing.FirstOrDefault(x => x.LinkId == r.LinkId);
                r.ParaId = comId;

                if (match == null)
                    {
                    result = AddReference(r, user.PPNumber);
                    }
                else if (match.ReferenceId != r.ReferenceId && r.LinkId.HasValue)
                    {
                    result = UpdateParaReference(comId, r.LinkId.Value, r.ReferenceId);
                    }
                }

            return string.IsNullOrEmpty(result) ? "Saved" : result;
            }
        }
    }
