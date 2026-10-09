
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public class TDSSlip
    {
        public MasterBase AssessmentYear { get; set; }
        public List<TDSSlip_Months> Months { get; set; }
        public List<TDSSlip_EmployeeList> EmployeeList { get; set; }
        public TDSSlip_Summary Summary { get; set; }
        public Url Url { get; set; }
        public int? CurrentSalaryPeriodCode { get; set; }
        public Filter filter { get; set; }
    }
    public class TDSSlip_Months
    {
        public int SalaryPeriodCode { get; set; }
        public string Month { get; set; }
    }
    public class TDSSlip_EmployeeList
    {

        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ACTIVE { get; set; }
        public int TotalCount { get; set; }
    }
    public class TDSSlip_Summary
    {
        public int SalaryPeriodCode { get; set; }
        public int GrossPay { get; set; }
        public int TotalDeduction { get; set; }
        public int NetPay { get; set; }
    }
}
