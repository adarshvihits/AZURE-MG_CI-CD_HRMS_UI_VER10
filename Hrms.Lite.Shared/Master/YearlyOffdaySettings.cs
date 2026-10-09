using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class YearlyOffdaySettings
    {
   
        public Guid OffdaySettingsGI { get; set; }
        public CalendarYear Year { get; set; }
        public string CombinationName { get; set; }
        public string Description { get; set; }
        public string mode { get; set; }
        public List<YearlyOffdaySettings> List { get; set; }
        public List<YearlyOffdaySettings_Days> Days { get; set; }
        public List<YearlyOffdaySettings_Dates> Dates { get; set; }
    }
    public class YearlyOffdaySettings_Days
    {
        public int DayCode { get; set; }
        public int WeekCode { get; set; }
        public string Days { get; set; }
        public bool Applicability { get; set; }
    }
    public class YearlyOffdaySettings_Dates
    {
        public string Date { get; set; }
        public string Day { get; set; }

    }
}
