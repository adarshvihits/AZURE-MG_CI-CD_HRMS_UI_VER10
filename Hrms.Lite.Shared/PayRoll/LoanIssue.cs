
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class LoanIssue
    {
        public int RequestCode { get; set; }


        public int LoanIssueCode { get; set; }
        public int EmpCode { get; set; }//sree
        public Guid LoanIssueGI { get; set; }
        //public UserBase User { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }

        public string LoanType { get; set; }
        public int LoanAmount { get; set; }
        public int Tabindex { get; set; }
        public int TotalTenure { get; set; }
        public decimal totalLoanAmount { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int MoreCount { get; set; }
        public int PendingTabCount { get; set; }
        public List<LoanIssueListTabs> LoanIssueListTabs { get; set; }
        public List<LoanIssue> LoanIssuesList { get; set; }
        public List<LoanIssue> BulkLoanIssuesList { get; set; }
        public Url Url { get; set; }

        public LoanIssueRequest LoanIssueRequest { get; set; }
        public LoanIssueDetails LoanIssueDetails { get; set; }
        public List<LoanIssueLog> LoanIssueLogList { get; set; }
        public string Mode { get; set; }
        public Filter Filter { get; set; }
        public LoanTopUpDetails LoanTopUpDetails { get; set; }
        public int LoanBalance { get; set; }
        public decimal MonthlyEMI { get; set; }
        public Filter SliderFilter { get; set; }
        public LoanType Loans { get; set; }
        public List<LoanRegister> LoanRegister { get; set; }
        public LoanRegister LoanRegister_Details { get; set; }
        public int Radio { get; set; }
        public int CompanyLoanAmount { get; set; }
        public int MaximumLimitPerEmployee { get; set; }
        public decimal BalanceAmount { get; set; }
        public int Tenure { get; set; }
       
        public int LoanSettingsCode { get; set; }
        public List<LoanRegister_Info> info { get; set; }
        public string EmployeeStatus { get; set; }

        public File Excel { get; set; }
        public string Message { get; set; }
        public string Amount { get; set; }
        public string EMI { get; set; }


        public int PrincipleAmount { get; set; }
        public int TopUpAmount { get; set; }
        public int AdjustmentAmount { get; set; }
        public int TotalMonthlyEMI { get; set; }
        public int AlreadyPaidAmount { get; set; }
        public int TotalBalanceAmount { get; set; }


        public string Name { get; set; }
        public string LoanBulkUploadingName { get; set; }
        public string Remarks { get; set; }

    }






    public class LoanIssueListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class LoanIssueRequest
    {
        public int RequestCode { get; set; }
        public Guid RequestGI { get; set; }
        public LoanType LoanType { get; set; }
        public string Pourpose { get; set; }
        public RepaymentMethod RepaymentMethod { get; set; }
        public int LoanAmount { get; set; }
        public int MonthlyEMI { get; set; }
        public int Tenure { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public PayrollMonth RepaymentStartMonth { get; set; }
        public string Remarks { get; set; }
        public List<LoanIssueLog> LoanIssueLog { get; set; }






    }
    public class LoanIssueLog
    {
        public int LoanIssueCode { get; set; }
        public Guid LoanIssueGI { get; set; }
        public int SlNo { get; set; }
        public string LoanType { get; set; }
        public string Pourpose { get; set; }
        public string RepaymentMethod { get; set; }
        public int MonthlyEMI { get; set; }
        public int Tenure { get; set; }
        public int LoanAmount { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }



  


}
    public class LoanIssueDetails
    {
        public int LoanIssueCode { get; set; }
        public Guid LoanIssueGI { get; set; }
        public LoanType LoanType { get; set; }
        public string Pourpose { get; set; }
        public RepaymentMethod RepaymentMethod { get; set; }
        public int LoanAmount { get; set; }
        public decimal MonthlyEMI { get; set; }
        public int Tenure { get; set; }
        public DateTime DisbursementDate { get; set; }
        public string RepaymentStartMonth { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public PayrollMonth PaymentStartMonth { get; set; }

  

}
    public class LoanType : MasterBase
    {

    }
    public class LoanTopUpDetails
    {


        public EmployeeHeader EmployeeHeader { get; set; }
        public int LoanIssueCode { get; set; }
        public int PrincipleAmount { get; set; }
        public int  MonthlyEMI { get; set; }
        public int TotalTenure { get; set; }
        public string DisbursementDate { get; set; }
        public string PaymentStarted { get; set; }
        public int PaidAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int BalanceTenure { get; set; }
        public int? TopUpAmount { get; set; }
        public int NewEMIAmount { get; set; }
        public int RemainingTenure { get; set; }
        public DateTime? NewDisbursementDate { get; set; }
        public int NewBalanceAmount { get; set; }
        public string Remarks { get; set; }
        public LoanType LoanType { get; set; }
        public int CmpLoanAmount { get; set; }
        public int MaxLimitPerEmployee { get; set; }
        public decimal BalanceAmt { get; set; }
        public int Tenure { get; set; }
    }

    public class LoanClosureDetails
    {
        public Guid LoanIssueGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int LoanIssueCode { get; set; }
        public int PrincipleAmount { get; set; }
        public int MonthlyEMI { get; set; }
        public int TotalTenure { get; set; }
        public string DisbursementDate { get; set; }
        public string PaymentStarted { get; set; }
        public int PaidAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int BalanceTenure { get; set; }
        public DateTime? ClosureDate { get; set; }
        public int? ClosureAmount { get; set; }
        public string Remarks { get; set; }

    }
    public class RepaymentMethod : MasterBase
    {

    }
    public class RepaymentStartMonth : MasterBase
    {

    }
    public class LoanRegister
    {
        public Guid LoanIssueGI { get; set; }
        public int LoanIssueCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string LoanSettingsName { get; set; }
        public int PrincipleAmount { get; set; }
        public int TopUpAmount { get; set; }
        public int AdjAmount { get; set; }
        public int MonthlyEMI { get; set; }
        public int AlreadyPaidAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int RemainingTenure { get; set; }
        public string LoanStatus { get; set; }
        public List<LoanRegister_Info> info { get; set; }
        public string EmployeeStatus { get; set; }
        public string Location { get; set; }
        public string Division { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int TotalTenure { get; set; }
    }
    public class LoanRegister_Info
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int CreditDebit { get; set; }

        public string Month { get; set; }
        public string Remarks { get; set; }
        public string C_D { get; set; }
        public DateTime DisbursementDate { get; set; }
    }

}

