        public List<FadDeskOfficerRptModel> GetFadDeskOfficerRptByDateRange(DateTime startDate, DateTime endDate)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<FadDeskOfficerRptModel> results = new List<FadDeskOfficerRptModel>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_RPT.P_GET_FAD_DESK_OFFICER_RPT_BY_DATES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_start_date", OracleDbType.Date).Value = startDate;
                cmd.Parameters.Add("p_end_date", OracleDbType.Date).Value = endDate;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                    while (reader.Read())
                        {
                        FadDeskOfficerRptModel rpt = new FadDeskOfficerRptModel
                            {
                            AuditPeriod = reader["AUDIT_PERIOD"]?.ToString(),
                            ChildCode = reader["CHILD_CODE"]?.ToString(),
                            CName = reader["C_NAME"]?.ToString(),
                            AZ = reader["AZ"]?.ToString(),
                            PName = reader["P_NAME"]?.ToString(),
                            Annex = reader["ANNEX"]?.ToString(),
                            GistOfParas = reader["GIST_OF_PARAS"]?.ToString(),
                            ParaNo = reader["PARA_NO"]?.ToString(),
                            NoOfInstances = reader["NO_OF_INSTANCES"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NO_OF_INSTANCES"]),
                            Risk = reader["RISK"]?.ToString(),
                            Amount = reader["AMOUNT"]?.ToString(),
                            };
                        results.Add(rpt);
                        }
                    }
                }
            con.Dispose();
            return results;
            }

