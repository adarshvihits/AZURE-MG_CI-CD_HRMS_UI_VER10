using System;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.AllowanceSettings
{
    public class AllowanceSettingsBatchUpdate : AllowanceSettingsSave
    {
        public Filter filter { get; set; }
        public Allowance Allowance { get; set; }
    }
}
