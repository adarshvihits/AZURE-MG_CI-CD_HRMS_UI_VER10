using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class MonthlyProfitBasedSettings
    {
        public Dropdown Month { get; set; }
        public string Location { get; set; }
        public string Product { get; set; }
        public decimal? Percentage { get; set; }
        public string PercentageString { get; set; }
        public List<MonthlyProfitBasedSettings> MonthlyProfitBasedSettingsList { get; set; }
        public File Excel { get; set; }
        public Filter Filter { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
    }
}
