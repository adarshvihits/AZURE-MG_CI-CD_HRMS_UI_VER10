using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SalaryRelease
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int PayrollGroup { get; set; }
        public Filter filter { get; set; }
        public int Month { get; set; }
        //public Guid EmployeeGuid { get; set; }
        //public string EmployeeID { get; set; }
        //public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string HoldMonth { get; set; }
        public decimal HoldAmount { get; set; }
        public string DOJ { get; set; }
        public string Mode { get; set; }
        public string AccNo { get; set; }
        public decimal Payable { get; set; }
        public decimal Recovery { get; set; }
        public decimal Amount { get; set; }
        public string AmountStr { get; set; }
        public decimal InitialHeldAmount { get; set; }
        public decimal TotalReleaseAmount { get; set; }
        public decimal CurrentHeldAmount { get; set; }
        public string HoldReason { get; set; }
        public decimal Balance { get; set; }
        public decimal ReleaseAmount { get; set; }
        public string Remarks { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int TotalAmount { get; set; }
        public List<SalaryRelease> SalaryReleaseList { get; set; }
        public List<SalaryReleaseHistory> ReleaseHistoryList { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
    }


    public class SalaryReleaseHistory
    {
        public string Designation { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string HeldMonth { get; set; }
        public decimal InitialHeldAmount { get; set; }
        public decimal ReleaseAmount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string HoldReason { get; set; }
        public string Remarks { get; set; }

    }
}
