
                    if (rdr["CODE"].ToString() != "" && rdr["CODE"].ToString() != null)
                        entity.CODE = Convert.ToInt32(rdr["CODE"]);

                    if (rdr["NAME"].ToString() != "" && rdr["NAME"].ToString() != null)
                        entity.NAME = rdr["NAME"].ToString();

                    entity.ACTIVE = rdr["ACTIVE"].ToString();

                    if (rdr["AUDITBY_ID"].ToString() != "" && rdr["AUDITBY_ID"].ToString() != null)
                        entity.AUDITBY_ID = Convert.ToInt32(rdr["AUDITBY_ID"]);

                    entity.AUDITBY_NAME = rdr["AUDITBY_NAME"].ToString();
                    entity.AUDITABLE = rdr["AUDITABLE"].ToString();
                    entity.ADDRESS = rdr["ADDRESS"].ToString();
                    entity.TELEPHONE = rdr["TELEPHONE"].ToString();
                    entity.EMAIL_ADDRESS = rdr["EMAIL_ADDRESS"].ToString();
        public List<AuditeeEntityUpdateModel> GetAuditeeEntitiesForAuthorization()
            {
            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeEntityUpdateModel> entitiesList = new List<AuditeeEntityUpdateModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_GetEntitees_for_update_authorization";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ENTITY_ID", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("E_up_status", OracleDbType.Varchar2).Value = "U";
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = "";
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditeeEntityUpdateModel entity = new AuditeeEntityUpdateModel();
                    if (rdr["ID"].ToString() != "" && rdr["ID"].ToString() != null)
                        entity.ID = Convert.ToInt32(rdr["ID"]);
                    if (rdr["ENTITY_ID"].ToString() != "" && rdr["ENTITY_ID"].ToString() != null)
                        entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    if (rdr["CODE"].ToString() != "" && rdr["CODE"].ToString() != null)
                        entity.CODE = Convert.ToInt32(rdr["CODE"]);
                    entity.NAME = rdr["NAME"].ToString();
                    entity.ACTIVE = rdr["ACTIVE"].ToString();
                    if (rdr["AUDITBY_ID"].ToString() != "" && rdr["AUDITBY_ID"].ToString() != null)
                        entity.AUDITBY_ID = Convert.ToInt32(rdr["AUDITBY_ID"]);
                    entity.AUDITABLE = rdr["AUDITABLE"].ToString();
                    entity.ADDRESS = rdr["ADDRESS"].ToString();
                    entity.TELEPHONE = rdr["TELEPHONE"].ToString();
                    entity.EMAIL_ADDRESS = rdr["EMAIL_ADDRESS"].ToString();
                    if (rdr["RISK_ID"].ToString() != "" && rdr["RISK_ID"].ToString() != null)
                        entity.RISK_ID = Convert.ToInt32(rdr["RISK_ID"]);
                    if (rdr["SIZE_ID"].ToString() != "" && rdr["SIZE_ID"].ToString() != null)
                        entity.SIZE_ID = Convert.ToInt32(rdr["SIZE_ID"]);
                    entity.UP_STATUS = rdr["UP_STATUS"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public string UpdateAuditeeEntity(AuditeeEntityUpdateModel entityModel, string IND)
            {
            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            string resp = "";
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_UPDATE_ENTITIES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_entity_id", OracleDbType.Int32).Value = entityModel.ENTITY_ID;
                cmd.Parameters.Add("E_code", OracleDbType.Int32).Value = entityModel.CODE;
                cmd.Parameters.Add("E_name", OracleDbType.Varchar2).Value = entityModel.NAME;
                cmd.Parameters.Add("E_active", OracleDbType.Varchar2).Value = entityModel.ACTIVE;
                cmd.Parameters.Add("E_auditby_id", OracleDbType.Int32).Value = entityModel.AUDITBY_ID;
                cmd.Parameters.Add("E_auditable", OracleDbType.Varchar2).Value = entityModel.AUDITABLE;
                cmd.Parameters.Add("E_address", OracleDbType.Varchar2).Value = entityModel.ADDRESS;
                cmd.Parameters.Add("E_telephone", OracleDbType.Varchar2).Value = entityModel.TELEPHONE;
                cmd.Parameters.Add("E_email_address", OracleDbType.Varchar2).Value = entityModel.EMAIL_ADDRESS;
                cmd.Parameters.Add("E_risk_id", OracleDbType.Int32).Value = entityModel.RISK_ID;
                cmd.Parameters.Add("E_size_id", OracleDbType.Int32).Value = entityModel.SIZE_ID;
                cmd.Parameters.Add("E_up_status", OracleDbType.Varchar2).Value = entityModel.UP_STATUS;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["remarks"].ToString();
                    }
                }
            con.Dispose();
            return resp;

            }

