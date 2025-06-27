namespace AIS.Models.IID
{
    public class HeadReviewModel
    {
        public int ComplaintId { get; set; }
        public string Directions { get; set; }
        public int AssignedUserId { get; set; }
        public string Comments { get; set; }
        public bool ReferBack { get; set; }
    }
}
