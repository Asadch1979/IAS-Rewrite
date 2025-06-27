using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace AIS.Models.IID
{
    public class InquiryReportModel
    {
        public int ComplaintId { get; set; }
        public string ReportText { get; set; }
        public IFormFile ReportFile { get; set; }
        public List<IFormFile> Evidence { get; set; }
    }
}
