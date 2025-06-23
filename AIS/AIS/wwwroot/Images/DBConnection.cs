        public string ReviewerAddChangeStatusRequestForSettledPara(string REFID, string REMARKS, string Action_IND)
                cmd.Parameters.Add("ind", OracleDbType.Varchar2).Value = Action_IND;
        public string AuthorizerAddChangeStatusRequestForSettledPara(string REFID, int NEW_STATUS, string REMARKS, string Action_IND)
                cmd.Parameters.Add("indicator", OracleDbType.Varchar2).Value = Action_IND;
        public string AddAuthorizeChangeStatusRequestForSettledPara(string REFID, string OBS_ID, string Action_IND)
                cmd.Parameters.Add("ind", OracleDbType.Varchar2).Value = Action_IND;
