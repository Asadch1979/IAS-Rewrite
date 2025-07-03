using System;

namespace AIS.Models
    {
    public class ManageAuditPara
        {
        public int ComId { get; set; }
        public int? OldParaId { get; set; }
        public int? NewParaId { get; set; }
        public int ParaNo { get; set; }
        public DateTime? AuditPeriod { get; set; }
        public string GistOfParas { get; set; }
        public string Risk { get; set; }
        public int RiskId { get; set; }
        public string Ind { get; set; }
        public int Annex { get; set; }
        public int? AnnexId { get; set; }
        public string ParaText { get; set; }
        public decimal Amount { get; set; }     // Always zero per procedure
        public int? NoInstances { get; set; }
        public int? AnnexRefId { get; set; }
        }
    }