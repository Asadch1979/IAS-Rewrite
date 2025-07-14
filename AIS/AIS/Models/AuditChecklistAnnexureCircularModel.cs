using System;

namespace AIS.Models
    {
    public class AuditChecklistAnnexureCircularModel
        {
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
