namespace AIS.Models
    {
    public class AuditeeEntityUpdateModel
        {
        public int? ID { get; set; }
        public int? ENTITY_ID { get; set; }
        public int? CODE { get; set; }
        public string NAME { get; set; }
        public string ACTIVE { get; set; }
        public int? AUDITBY_ID { get; set; }
        public string AUDITBY_NAME { get; set; }
        public string AUDITABLE { get; set; }
        public string ADDRESS { get; set; }
        public string TELEPHONE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public int? RISK_ID { get; set; }
        public int? SIZE_ID { get; set; }
        public string UP_STATUS { get; set; }
        public string ERISK { get; set; }
        public string ESIZE { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATE_ON { get; set; }
        public string AUTHORIZED_BY { get; set; }
        public string AUTHORIZED_ON { get; set; }
        }
    }