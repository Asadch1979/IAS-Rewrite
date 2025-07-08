namespace AIS.Models
    {
    namespace AIS.Models
        {
        public class AuditeeOldParasPpnoModel
            {
            public int? ComId { get; set; }
            public int? OldParaId { get; set; }
            public int? NewParaId { get; set; }
            public string EntityName { get; set; }
            public string AuditPeriod { get; set; }
            public decimal? Amount { get; set; }
            public string Annex { get; set; }
            public string ParaStatus { get; set; }    // Always 'Un-Settled' per query
            public string Ind { get; set; }
            public string ParaNo { get; set; }
            public string GistOfParas { get; set; }
            }
        }

    }
