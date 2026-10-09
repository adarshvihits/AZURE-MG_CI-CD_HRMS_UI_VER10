using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class InsurancePolicy : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public MasterBase InsuranceCompany { get; set; }
        public List<InsurancePolicy> InsurancePolicyList { get; set; }
        public List<InsuranceCompany> InsuranceCompanyLt { get; set; }
    }
    public class InsuranceCompany : MasterBase
    {
        public List<InsuranceCompany> InsuranceCompanyList { get; set; }
    }
}
