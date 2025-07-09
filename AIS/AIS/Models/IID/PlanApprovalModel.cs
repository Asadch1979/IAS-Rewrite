namespace AIS.Models.IID
    {
    public class PlanApprovalModel
        {
        public int PlanId { get; set; }
        public int ApprovedBy { get; set; }
        public string IsApproved { get; set; }
        public string EditedPlan { get; set; }
        public string FurtherActions { get; set; }
        }

    }
