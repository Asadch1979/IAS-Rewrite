                    grp.FUNCTION_ID_1 = rdr["function_id_1"].ToString();
                    grp.FUNCTION_1 = rdr["function_1"].ToString();
                    grp.FUNCTION_ID_2 = rdr["function_id_2"].ToString();
                    grp.FUNCTION_2 = rdr["function_2"].ToString();
        public string AddAnnexure(string ANNEX_CODE = "", string HEADING = "", int PROCESS_ID = 0, int FUNCTION_OWNER_ID = 0, int FUNCTION_ID_1 = 0, int FUNCTION_ID_2 = 0, int RISK_ID = 0, string MAX_NUMBER = "", string GRAVITY = "", string WEIGHTAGE = "")
                cmd.Parameters.Add("FUNCTION_ID_1", OracleDbType.Int32).Value = FUNCTION_ID_1;
                cmd.Parameters.Add("FUNCTION_ID_2", OracleDbType.Int32).Value = FUNCTION_ID_2;
        public string UpdateAnnexure(int ANNEX_ID = 0, string HEADING = "", int PROCESS_ID = 0, int FUNCTION_OWNER_ID = 0, int FUNCTION_ID_1 = 0, int FUNCTION_ID_2 = 0, int RISK_ID = 0, string MAX_NUMBER = "", string GRAVITY = "", string WEIGHTAGE = "")
                cmd.Parameters.Add("FUNCTION_ID_1", OracleDbType.Int32).Value = FUNCTION_ID_1;
                cmd.Parameters.Add("FUNCTION_ID_2", OracleDbType.Int32).Value = FUNCTION_ID_2;
