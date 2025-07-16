using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace AIS.Models.IID
    {
    public class ComplaintModel
        {
        public string Nature { get; set; }
        public string Category { get; set; }
        public string ReceivedFrom { get; set; }
        public int LocationTypeId { get; set; }
        public int? GMOfficeId { get; set; }
        public int? RegionId { get; set; }
        public int? BranchId { get; set; }
        public string ComplainantName { get; set; }
        public string CNIC { get; set; }
        public string CellularNumber { get; set; }
        public string MailingAddress { get; set; }
        public string Gender { get; set; }
        public string ComplaintNo { get; set; }
        public string Contents { get; set; }
        public string UploadedComplaint { get; set; }
        public string UploadedFFR { get; set; }
        public string UploadedEvidence { get; set; }
        public string ActionRequired { get; set; }
        public int SubmittedBy { get; set; }
        }

    }
