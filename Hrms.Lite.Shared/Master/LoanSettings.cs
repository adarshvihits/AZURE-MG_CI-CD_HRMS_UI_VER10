using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class LoanSettings : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<LoanSettings> LoanSettingsList { get; set; }
        public UserBase User { get; set; }
        public RepaymentMethod RepaymentMethod { get; set; }
        public int?[] RepaymentMethods { get; set; }
        public int CompanyLoanAmount { get; set; }
        public int MaximumLimitPerEmployee { get; set; }
        public int MaximumTenure { get; set; }
        public string AdvType { get; set; }
    }

}
