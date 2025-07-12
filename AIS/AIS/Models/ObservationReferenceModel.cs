using System;

namespace AIS.Models
{
    public class ObservationReferenceModel
    {
        public int ComId { get; set; }
        public int EntId { get; set; }
        public string ParaTitle { get; set; }
        public int? ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public int? AssignedAuditorId { get; set; }
        public string Status { get; set; }
    }
}
