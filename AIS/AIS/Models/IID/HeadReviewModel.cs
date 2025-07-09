namespace AIS.Models.IID
    {
    public class HeadReviewModel
        {
        public int ComplaintId { get; set; }
        public int AssessmentId { get; set; }
        public int ReviewedBy { get; set; }
        public string Directions { get; set; }
        public int AssignedToUnit { get; set; }
        public string ReferredBackComments { get; set; }
        public string Action { get; set; }
        }

    }
