namespace AIS.Models
    {
    public class FADAuditParasReviewModel
        {
        public string OLD_PARA_ID { get; set; }
        public string NEW_PARA_ID { get; set; }
        public string ANNEX { get; set; }
        public string PROCESS { get; set; }
        public string SUB_PROCESS { get; set; }
        public string CHECK_LIST { get; set; }
        public string MEMO_NO { get; set; }
        public string PARA_NO { get; set; }
        public string PARA_TEXT { get; set; }
        public string OBS_GIST { get; set; }
        public string OBS_RISK { get; set; }
        public string OBS_RISK_ID { get; set; }
        public string AMOUNT_INV { get; set; }
        public string NO_INSTANCES { get; set; }
        public string PPNO { get; set; }
        public string RESP_ROLE { get; set; }
        public string RESP_AMOUNT { get; set; }
        public string LOAN_CASE { get; set; }
        public string REPORT_ID { get; set; }
        public string REPORT_NAME { get; set; }

        public string AUDITEE_REPLY { get; set; }
        public string AUDITOR_COMMENTS { get; set; }
        public string HEADCOMMENTS { get; set; }
        public string ROOT_CAUSE { get; set; }

        }
    }
