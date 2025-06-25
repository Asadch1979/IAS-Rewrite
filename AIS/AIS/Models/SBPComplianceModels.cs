using System;

namespace AIS.Models
{
    public class SBPObservation
    {
        public int OBSERVATION_ID { get; set; }
        public string OBSERVATION_TEXT { get; set; }
        public int DIVISION_ID { get; set; }
        public string STATUS { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime CREATED_ON { get; set; }
        public string ATTACHMENT_PATH { get; set; }
        public DateTime DATE_RECEIVED { get; set; }
    }

    public class SBPAssignment
    {
        public int ASSIGNMENT_ID { get; set; }
        public int OBSERVATION_ID { get; set; }
        public string ASSIGNED_TO_ROLE { get; set; }
        public int ASSIGNED_TO_ID { get; set; }
        public string INSTRUCTIONS { get; set; }
        public int ASSIGNED_BY { get; set; }
        public DateTime ASSIGNED_ON { get; set; }
        public string STATUS { get; set; }
    }

    public class SBPResponse
    {
        public int RESPONSE_ID { get; set; }
        public int OBSERVATION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string RESPONSE_TEXT { get; set; }
        public int SUBMITTED_BY { get; set; }
        public DateTime SUBMITTED_ON { get; set; }
        public string STATUS { get; set; }
        public string ATTACHMENT_PATH { get; set; }
    }

    public class SBPReviewHistory
    {
        public int OBSERVATION_ID { get; set; }
        public string REVIEWER_ROLE { get; set; }
        public int REVIEWER_ID { get; set; }
        public string COMMENTS { get; set; }
        public string ACTION { get; set; }
        public DateTime REVIEWED_ON { get; set; }
    }
}
