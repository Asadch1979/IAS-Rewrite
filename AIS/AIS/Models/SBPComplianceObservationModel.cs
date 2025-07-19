namespace AIS.Models
    {
    public class SBPComplianceObservationModel
        {
        public int Id { get; set; }
        public string Observation { get; set; }
        public string AssignedDivision { get; set; }
        public string AssignedDepartment { get; set; }
        public string DepartmentResponse { get; set; }
        public string DivisionalHeadComment { get; set; }
        public string GroupHeadComment { get; set; }
        public string COOComment { get; set; }
        public string Status { get; set; }
        }
    }
