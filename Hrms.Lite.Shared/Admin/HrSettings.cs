using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
    public class HrSettings
    {
        
        public int SettingsCode { get; set; }
        public string Roles { get; set; }
        public Guid SettingsGI { get; set; }
        public Boolean SettingsApplicabilty { get; set; }
        public decimal SecondPayout { get; set; }
        public decimal AdditionalBonus { get; set; }
        public Dropdown Financialyear { get; set; }
        public Dropdown Financialyear2 { get; set; }
        public List<HrSettings> HrSettingsList { get; set; }
        public string Mode { get; set; }
        public bool Active { get; set; }

        public string SettingsName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
