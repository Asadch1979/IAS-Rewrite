namespace AIS.Models
{
    /// <summary>
    /// Represents a staff member position within the audit department.
    /// These fields map directly to the Oracle table T_AU_STAFF_POSITION
    /// and the PKG_HR package procedures.
    /// </summary>
    public class StaffPosition
    {
        public int Id { get; set; }
        public string PPNO { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Designation { get; set; }
        public string Placement { get; set; }
        public string HighestQualification { get; set; }
        public string Specialization { get; set; }
        public string AuditCertification { get; set; }
        public decimal TotalExperience { get; set; }
        public decimal AuditExperience { get; set; }
        public int ZoneId { get; set; }
        public string Company { get; set; }
    }
}
