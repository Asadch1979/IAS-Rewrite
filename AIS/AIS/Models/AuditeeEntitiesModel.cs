namespace AIS.Models
    {
    public class AuditeeEntitiesModel
        {
        public int? ENTITY_ID { get; set; }
        public int? CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string NAME { get; set; }
        public string ACTIVE { get; set; }
        public int? TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int? AUDITBY_ID { get; set; }

        public string AUDITBY_NAME { get; set; }

        public string AUDITABLE { get; set; }

        public string STATUS { get; set; }


        public int? INSPECTEDBY_ID { get; set; }
        public string COST_CENTER { get; set; }
        public string AUDITOR { get; set; }
        public string IAD { get; set; }
        public string COMPLICE_BY { get; set; }
        public string COMPLIANCE_UNIT { get; set; }
        public string ADDRESS { get; set; }
        public string TELEPHONE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string ERISK { get; set; }
        public string ESIZE { get; set; }
        public int? RISK_ID { get; set; }
        public int? SIZE_ID { get; set; }
        public int? ENG_ID { get; set; }

        public string COM_BY { get; set; }
        }
    }
