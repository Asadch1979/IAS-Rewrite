namespace AIS.Models
{
    public class ReferenceEntitySummaryModel
    {
        public int EntityId { get; set; }
        public string EntityCode { get; set; }
        public string EntityName { get; set; }
        public string AuditPeriod { get; set; }
        public int TotalParas { get; set; }
        public int UpdatedParas { get; set; }
        public int Pendency { get; set; }
    }
}
