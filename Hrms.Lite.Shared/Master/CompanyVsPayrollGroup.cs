using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class CompanyVsPayrollGroup
    {
        public int Code { get; set; }

        public int GroupCompanyCode { get; set; }
        public List<CompanyVsPayrollGroupList> CompanyVsPayrollGroupList { get; set; }
    }
    public class CompanyVsPayrollGroupList
    {
        
        public int PayrollGroupCode { get; set; }
        public string Name { get; set; }
        public bool Applicable { get; set; }
        public int GroupCompanyCode { get; set; }
        public int Code { get; set; }
    }
}
