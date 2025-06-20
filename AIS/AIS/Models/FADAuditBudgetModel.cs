namespace AIS.Models
{
    public class FADAuditBudgetModel
    {
        public int ID { get; set; }
        public string GL_CODE { get; set; }
        public string GL_HEADING { get; set; }
        public string EXISTING { get; set; }
        public string UTILIZATION { get; set; }
        public string REMAND { get; set; }
        public string RATIONALIZATION { get; set; }
        public string STATUS { get; set; }
    }
}
