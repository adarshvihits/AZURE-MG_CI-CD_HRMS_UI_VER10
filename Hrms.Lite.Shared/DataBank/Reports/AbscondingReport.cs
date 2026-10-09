using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class AbscondingReport
    {
        public Guid EmployeeGI { get; set; }
        public Filter Filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public MasterBase Reason { get; set; }
        public AbscondingReporttList Details { get; set; }  
        public List<AbscondingReporttList> AbscondingReporttList { get; set; }
        public List<AbscoundingEmployeeSalaryDetails> SalaryDetails { get; set; }
        public Dropdown AbscondingDay { get; set; }
    }
    public class AbscondingReporttList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string ReportingManager { get; set; }
        public string MailID { get; set; }
        public string Phone { get; set; }
        public DateTime? DOJ { get; set; }
        public string LastProcessedMonth { get; set; }
        public DateTime? LastReportingDate { get; set; }
        public DateTime? LWD { get; set; }
        public string LastPunchDate { get; set; }
        public int Ageing { get; set; }
        public decimal NoOfDays { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        

    }
    public class AbscoundingEmployeeSalaryDetails
    {
        public SalaryPeriod Month { get; set; }
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int NetPay { get; set; }
    }
}
