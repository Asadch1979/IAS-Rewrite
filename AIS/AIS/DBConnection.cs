        public AuditReportReviewModel GetAuditReportForReview(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            AuditReportReviewModel resp = new AuditReportReviewModel();
            var loggedInUser = sessionHandler.GetSessionUser();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pg_fad.P_Get_audit_reports_for_review";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        resp = new AuditReportReviewModel();
                        var clob = rdr.GetOracleClob(rdr.GetOrdinal("AUDIT_REPORT"));
                        if (clob != null)
                            resp.AUDIT_REPORT = clob.Value;
                        }
                    }
                }
            con.Dispose();
            return resp;
            }

