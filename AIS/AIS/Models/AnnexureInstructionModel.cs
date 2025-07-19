using System;

namespace AIS.Models
    {
    public class AnnexureInstructionModel
        {
        public string InstructionsTitle { get; set; }
        public string annexureId { get; set; }
        public string referenceTypeId { get; set; }
        public string referenceType { get; set; }
        public DateTime? InstructionsDate { get; set; }
        public string InstructionsDetails { get; set; }
        public int AnnexureRefId { get; set; }
        public string IND { get; set; }
        public string Status { get; set; }
        }
    }
