namespace AIS.Models
{
    public class EntityTaskSummaryModel
    {
        public int EntityId { get; set; }
        public string EntityCode { get; set; }
        public string EntityName { get; set; }
        public int AuditYear { get; set; }
        public int TotalParas { get; set; }
        public int ParasUpdated { get; set; }
    }
}
