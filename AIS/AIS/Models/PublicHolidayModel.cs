using System;

namespace AIS.Models
    {
    public class PublicHolidayModel
        {
        public int ID { get; set; }
        public DateTime HOLIDAY_DATE { get; set; }
        public int HOLIDAY_YEAR { get; set; }
        public string IS_WEEKEND { get; set; }  // "Y"/"N"
        public string IS_HOLIDAY { get; set; }  // "Y"/"N"
        public string HOLIDAY_NAME { get; set; }
        public string DAT { get; set; }  // "N" - Normal, "F" - Festival, "O" - Other
        }

    }
