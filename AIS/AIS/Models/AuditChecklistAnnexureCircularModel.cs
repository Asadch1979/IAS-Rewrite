using System;

namespace AIS.Models
    {
    public class AuditChecklistAnnexureCircularModel
        {
        /// <summary>
        /// Identifier of <c>TBL_PARA_REFERENCE_LINKS</c> row when the circular
        /// is associated with a para. Null for unlinked references.
        /// </summary>
        public int? LinkId { get; set; }
        public int ID { get; set; }
        public int DivisionEntId { get; set; }
        public int ReferenceTypeId { get; set; }
        public string ReferenceType { get; set; }
        public string InstructionsDetails { get; set; }
        public string Keywords { get; set; }
        public string RedirectedPage { get; set; }
        public string Division { get; set; }
        public string InstructionsTitle { get; set; }
        public DateTime? InstructionsDate { get; set; }
        public string DocType { get; set; }
        }

    }
