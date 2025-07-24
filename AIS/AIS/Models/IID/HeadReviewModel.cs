namespace AIS.Models.IID
    {
    public class HeadReviewModel
        {
        public int ComplaintId { get; set; }
        public int AssessmentId { get; set; }
        public int ReviewedBy { get; set; }
        public string Directions { get; set; }
        public int AssignedToUnit { get; set; }
        public int TeamLeadId { get; set; }
        public string TeamMembers { get; set; }
        public string AssignedOn { get; set; }
        public string DueDate { get; set; }
        public string ReferredBackComments { get; set; }
        public string Action { get; set; }
        }

    }
