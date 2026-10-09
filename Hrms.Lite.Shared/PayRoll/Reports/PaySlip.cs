using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PaySlip
    {
        public FinancialYear FinancialYear { get; set; }
        public List<PaySlip_Months> Months { get; set; }
        public List<PaySlip_EmployeeList> EmployeeList { get; set; }
        public PaySlip_Summary Summary { get; set; }
        public Url Url { get; set; }
        public int? CurrentSalaryPeriodCode { get; set; }
        public Filter filter { get; set; }
    }
    public class PaySlip_Months
    {
        public int SalaryPeriodCode { get; set; }
        public string Month { get; set; }
    }
    public class PaySlip_EmployeeList
    {       

        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ACTIVE { get; set; }
        public int TotalCount { get; set; }
    }
    public class PaySlip_Summary
    {
        public int SalaryPeriodCode { get; set; }
        public int GrossPay { get; set; }
        public int TotalDeduction { get; set; }
        public int NetPay { get; set; }
    }
}

