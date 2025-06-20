namespace AIS.Models
    {
    public class AuditeeEntityUpdateModel
        {
        public int? ID { get; set; }
        public int? ENTITY_ID { get; set; }
        public int? CODE { get; set; }
        public int? CODE_OLD { get; set; }
        public string DESCRIPTION { get; set; }
        public string DESCRIPTION_OLD { get; set; }
        public string NAME { get; set; }
        public string NAME_OLD { get; set; }
        public string ACTIVE { get; set; }
        public string ACTIVE_OLD { get; set; }
        public int? TYPE_ID { get; set; }
        public int? TYPE_ID_OLD { get; set; }
        public string TYPE_NAME { get; set; }
        public string TYPE_NAME_OLD { get; set; }
        public int? AUDITBY_ID { get; set; }
        public int? AUDITBY_ID_OLD { get; set; }

        public string AUDITBY_NAME { get; set; }
        public string AUDITBY_NAME_OLD { get; set; }

        public string AUDITABLE { get; set; }
        public string AUDITABLE_OLD { get; set; }

        public string STATUS { get; set; }
        public string STATUS_OLD { get; set; }


        public int? INSPECTEDBY_ID { get; set; }
        public string UP_STATUS { get; set; }
        public string AUDITOR { get; set; }
        public string IAD { get; set; }
        public string COMPLICE_BY { get; set; }
        public string COMPLIANCE_UNIT { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS_OLD { get; set; }
        public string TELEPHONE { get; set; }
        public string TELEPHONE_OLD { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string EMAIL_ADDRESS_OLD { get; set; }
        public string ERISK { get; set; }
        public string ERISK_OLD { get; set; }
        public string ESIZE { get; set; }
        public string ESIZE_OLD { get; set; }
        public int? RISK_ID { get; set; }
        public int? RISK_ID_OLD { get; set; }
        public int? SIZE_ID { get; set; }
        public int? SIZE_ID_OLD { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATE_ON { get; set; }
        public string AUTHORIZED_BY { get; set; }
        public string AUTHORIZED_ON { get; set; }
        }
    }