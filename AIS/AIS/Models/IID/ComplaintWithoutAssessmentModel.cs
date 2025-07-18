namespace AIS.Models.IID
{
    public class ComplaintWithoutAssessmentModel
    {
        public int ComplaintId { get; set; }
        public string Nature { get; set; }
        public string Contents { get; set; }
        public string UploadedComplaint { get; set; }
        public string UploadedFFR { get; set; }
        public string UploadedEvidence { get; set; }
        public string ActionRequired { get; set; }
        public string Status { get; set; }
        public int SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public int LocationTypeId { get; set; }
        public int? GMOfficeId { get; set; }
        public int? RegionId { get; set; }
        public int? BranchId { get; set; }
    }
}
