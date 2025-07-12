using System;

namespace AIS.Models
{
    public class UpdateLogModel
    {
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ActionType { get; set; }
    }
}
