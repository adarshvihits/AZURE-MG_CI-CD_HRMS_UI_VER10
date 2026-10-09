using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class ReferralIncentiveReport
    {
        public Dropdown PayrollMonth { get; set; }
        public FilterMultiSelect filter { get; set; }
        public List<ReferralIncentiveReportList> List { get; set; }
    }

    public class ReferralIncentiveReportList
    {
        public Guid EmployeeGI { get; set; }
        public int ReferredEmployeeCode { get; set; }
        public int Referralincentivecode { get; set; }
        public string ReferredEmployeeID { get; set; }
        public string ReferredemployeeName { get; set; }
        public string ReferredemployeeLocation { get; set; }
        public string ReferredemployeeDepartment { get; set; }
        public string ReferredemployeeDesignation { get; set; }
        public int CandidateEmployeecode { get; set; }
        public string CandidateEmployeeID { get; set; }
        public int Referralamount { get; set; }
        public string CandidateEmployeeName { get; set; }
        public int payoutstatus { get; set; }
    }
}
