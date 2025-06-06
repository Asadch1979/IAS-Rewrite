
        public string AddExceptionAccountReport(string IND = "A", int REPORT_ID = 0, string REPORT_TITLE = "", string DESCRIPTION = "", string TYPE = "")
            {
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_Add_new_exp_report";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("REPORT_ID", OracleDbType.Int32).Value = REPORT_ID;
                cmd.Parameters.Add("REPORT_TITLE", OracleDbType.Varchar2).Value = REPORT_TITLE;
                cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = DESCRIPTION;
                cmd.Parameters.Add("TYPE", OracleDbType.Varchar2).Value = TYPE;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
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
