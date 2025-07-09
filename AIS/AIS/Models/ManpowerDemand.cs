using System;

namespace AIS.Models
    {
    /// <summary>
    /// Represents a manpower demand raised by an audit zone.
    /// </summary>
    public class ManpowerDemand
        {
        public int Id { get; set; }
        public string Rank { get; set; }
        public string Placement { get; set; }
        public int Existing { get; set; }
        public int AdditionalRequired { get; set; }
        public int ZoneId { get; set; }
        public string Status { get; set; }
        public int SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string HeadFadRemarks { get; set; }
        }
    }
