using System;

namespace AIS.Models
    {
    public class VersionHistoryModel
        {
        public int VersionId { get; set; }
        public string VersionNo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string IsActive { get; set; }
        }
    }
