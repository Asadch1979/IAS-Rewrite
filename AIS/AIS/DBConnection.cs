                    chk.NEW_PARA_ID = rdr["ID"].ToString();
                    chk.NEW_PARA_ID = rdr["REF_P"].ToString();
                cmd.Parameters.Add("REFID", OracleDbType.Varchar2).Value = NEW_PARA_ID;
                            cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = NEW_PARA_ID;
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = NEW_PARA_ID;
        public string AddAuthorizeChangeStatusRequestForSettledPara(string NEW_PARA_ID, string OBS_ID, string IND, string Action_IND)
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = NEW_PARA_ID;
        public string AddChangeStatusRequestForPara(string NEW_PARA_ID, int NEW_STATUS, string REMARKS, string IND, string Action_IND)
                cmd.Parameters.Add("obs_id", OracleDbType.Varchar2).Value = NEW_PARA_ID;
