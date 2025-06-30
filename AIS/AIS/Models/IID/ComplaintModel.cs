using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace AIS.Models.IID
{
    public class ComplaintModel
    {
        public string Nature { get; set; }
        public string Contents { get; set; }
        public IFormFile ComplaintFile { get; set; }
        public IFormFile FfrFile { get; set; }
        public List<IFormFile> OtherEvidence { get; set; }
        public string ActionRequired { get; set; }
    }
}
