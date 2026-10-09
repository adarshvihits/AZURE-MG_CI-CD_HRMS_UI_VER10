using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
 
        public class LOPSettings
        {
       
            public int AllowanceCode { get; set; }
            public string Name { get; set; }
            public bool LopStatus { get; set; }
            public bool LateHourStatus { get; set; }
            public bool EsiLeaveStatus { get; set; }
            public bool SuspensionStatus { get; set; }
            public bool LayOffStatus { get; set; }
            public bool MaternityLeaveStatus { get; set; }
            public decimal? LopPercent { get; set; }
            public decimal? LateHourPercent { get; set; }
            public decimal? EsiLeavePercent { get; set; }
            public decimal? SuspensionPercent { get; set; }
            public decimal? LayOffPercent { get; set; }   
        public decimal? MaternityLeavePercent { get; set; }
        public List<LOPSettings> LOPSettingsList { get; set; }
        }
}

