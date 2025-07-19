using System;

namespace AIS.Models
{
    /// <summary>
    /// Represents a link between a para and its reference.
    /// Maps to TBL_PARA_REFERENCE_LINKS.
    /// </summary>
    public class ParaReferenceLinkModel
    {
        public int? LinkId { get; set; }
        public int EntityId { get; set; }
        public int OldParaId { get; set; }
        public int NewParaId { get; set; }
        public int ParaId { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceTitle { get; set; }
        public int? CreditManualId { get; set; }
        public int? OpManualId { get; set; }
        public string ManualType { get; set; }
        public string Chapter { get; set; }
        public string MatchedText { get; set; }
        public string LinkType { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
