using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class SettingsDashBoard
    {
        public string Name { get; set; }
        public int ItemCount { get; set; }
        public string LastUpdated { get; set; }
        public List<SettingsDashBoard> SettingsDashBoardList { get; set; }
        public List<GeneralSettings> GeneralSettingsList { get; set; }
        public DashBoardCount DashBoardCount { get; set; }


    }
    public class GeneralSettings
    {
        public string Name { get; set; }
        public int ItemCount { get; set; }

    }
    public class DashBoardCount
    {
        public int GroupCompanyCount { get; set; }
        public int EmployeeCount { get; set; }
        public int HRUserCount { get; set; }
        public int ESSPUserCount { get; set; }
        public int MISUserCount { get; set; }
        public int HolidayCount { get; set; }

    }
}
