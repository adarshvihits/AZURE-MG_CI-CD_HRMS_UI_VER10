using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
   public class CoffBalanceReport
    {
        public UserBase User { get; set; }
        public Filter filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Category { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public decimal Balance { get; set; }
        public decimal Taken { get; set; }
        public decimal Expired { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

        public List<CoffBalanceReport> CoffBalanceReportList { get; set; }
        public List<CoffBalanceReport_Details> Details { get; set; }

    }


    public class CoffBalanceReport_Details
    {
        public string Date { get; set; }
        public decimal Balance { get; set; }
        public DateTime Expiry { get; set; }

    }
    public class CoffBalance
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public MasterBase LeavePeriod { get; set; }
        public DateTime EntitleDate { get; set; }
        public DateTime AvailedDate { get; set; }
        public string EntitlementType { get; set; }
        public string CreditedFor { get; set; }
        public decimal AvailableCredit { get; set; }
        public DateTime ExpiresOn { get; set; }
        public decimal Expired { get; set; }
        public decimal Availed { get; set; }
        public decimal Balance { get; set; }
        public List<CoffBalance> CoffBalanceList { get; set; }
        public int CoffLapseDays { get; set; }
        public string Session { get; set; }
        public string Day { get; set; }
    }
}

