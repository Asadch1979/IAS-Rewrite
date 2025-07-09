using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace AIS.Models.IID
    {
    public class ComplaintModel
        {
        public string Nature { get; set; }
        public string Contents { get; set; }
        public string UploadedComplaint { get; set; }
        public string UploadedFFR { get; set; }
        public string UploadedEvidence { get; set; }
        public string ActionRequired { get; set; }
        public int SubmittedBy { get; set; }
        }

    }
