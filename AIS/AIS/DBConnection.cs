        public string AddExceptionAccountReport(string IND, int REPORT_ID, string REPORT_TITLE, string DESCRIPTION, string TYPE, int LOAN_STATUS_ID)
                cmd.Parameters.Add("LOAN_STATUS_ID", OracleDbType.Int32).Value = LOAN_STATUS_ID;
