using System;

namespace AIS.Models
    {
    public class CAObservation
        {
        public int ObservationID { get; set; }
        public string ObservationText { get; set; }
        public DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
        public string CurrentStatus { get; set; }
        public int AssignedDivisionID { get; set; }
        public int AssignedDepartmentID { get; set; }
        public string LegacyFlag { get; set; }
        public int LegacyYear { get; set; }
        public string SupportingDocuments { get; set; }
        }

    public class CAObservationResponse
        {
        public int ResponseID { get; set; }
        public int ObservationID { get; set; }
        public string ResponseText { get; set; }
        public int RespondedBy { get; set; }
        public DateTime ResponseDate { get; set; }
        public string ForwardedToRole { get; set; }
        public string Status { get; set; }
        public string SupportingDocuments { get; set; }
        }

    public class CAObservationAuditTrail
        {
        public int AuditTrailID { get; set; }
        public int ObservationID { get; set; }
        public string Action { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remarks { get; set; }
        }
    }
