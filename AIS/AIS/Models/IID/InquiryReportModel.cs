using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
namespace AIS.Models.IID
    {
    public class InquiryReportModel
        {
        public int ComplaintId { get; set; }
        public string NameComplainant { get; set; }
        public string NameAccused { get; set; }
        public string Gist { get; set; }
        public string Proceedings { get; set; }
        public string Findings { get; set; }
        public string Recommendation { get; set; }
        public string UploadedReport { get; set; }
        public string UploadedEvidence { get; set; }
        public DateTime SubmittedOn { get; set; }
        public int SubmittedBy { get; set; }
        }

    }
