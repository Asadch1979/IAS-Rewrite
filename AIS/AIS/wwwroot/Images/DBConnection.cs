        public string ReviewerAddChangeStatusRequestForSettledPara(string REFID, string IND, string REMARKS, string Action_IND)
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
        public string AuthorizerAddChangeStatusRequestForSettledPara(string REFID, string IND, int NEW_STATUS, string REMARKS, string Action_IND)
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
        public string AddAuthorizeChangeStatusRequestForSettledPara(string REFID, string OBS_ID, string IND, string Action_IND)
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
