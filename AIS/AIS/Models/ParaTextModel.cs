using System;

namespace AIS.Models
    {
    public class ParaTextModel
        {
        public int? MEMO_NO { get; set; }
        public int? TEXT_ID { get; set; }
        public string MEMO_TXT { get; set; }
        public string BRANCH_REPLY { get; set; }
        public string CAU_INSTRUCTION { get; set; }
        public int ComId { get; set; }
        public int EntityId { get; set; }
        public int OldParaId { get; set; }
        public int NewParaId { get; set; }
        public string AuditPeriod { get; set; }
        public string ParaStatus { get; set; }
        public string AuditedBy { get; set; }
        public string Risk { get; set; }
        public string IND { get; set; }
        public string ParaNo { get; set; }
        public DateTime ParaAddedOn { get; set; }
        public string GistOfParas { get; set; }
        public string Text { get; set; }
        public string ParaText { get; set; }

        }
    }
