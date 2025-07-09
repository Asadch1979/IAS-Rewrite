namespace AIS.Models.IID
    {
    public class AnalysisModel
        {
        public int ReportId { get; set; }
        public string PolicyGaps { get; set; }
        public string ControlGaps { get; set; }
        public string ProceduralViolations { get; set; }
        public string Comments { get; set; }
        public bool ReferBack { get; set; }
        }
    }
