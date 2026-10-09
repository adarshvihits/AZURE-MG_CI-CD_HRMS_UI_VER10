using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class PayrollDashBoard
    {
        public int PayrollConfirmations { get; set; }
        public int PendingTask { get; set; }
        public int FandFPendingCount { get; set; }
        public int OutstandingLoanBalance { get; set; }
        public int PFApplicable { get; set; }
        public int PFNotApplicable { get; set; }
        public int PFVariance { get; set; }
        public int ESIApplicable { get; set; }
        public int ESINotApplicable { get; set; }
        public int ESIVariance { get; set; }
        public int SalaryVariance { get; set; }
        public int OtherVarience { get; set; }
        public int TotalSalaryYTD { get; set; }
        public int AverageSalary { get; set; }
        public List<PayrollDashBoardList> PayrollDashBoardList { get; set; }
        public List<PayrollDashBoardList> ESI_PFApplicable { get; set; }
        public List<PayrollDashBoardList> ESI_PF_NOT_Applicable { get; set; }
        public List<PayrollDashBoardList> ESI_PF_Variance_Applicable { get; set; }
        public List<PayrollDashBoardDiagram> PayrollDashBoardDiagram { get; set; }
    }

    public class PayrollDashBoardList
    {
        public int EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime LWD { get; set; }
        public int Aging { get; set; }
        public string SalaryProcessingStatus { get; set; }
        public decimal HeldAmount { get; set; }
        public string FnFStatus { get; set; }
        public string LoanType { get; set; }
        public int LoanBalance { get; set; }
        public int RemainingTenure { get; set; }
        public decimal OldGross { get; set; }
        public decimal NewGross { get; set; }
        public decimal Difference { get; set; }
        public string Reason { get; set; }
        public string ChangeDoneBy { get; set; }
        public string VarianceIn { get; set; }
        public string OldValue { get; set; }
        public string CurrentValue { get; set; }
        public string Type { get; set; }
        public string Count { get; set; }
        public string PfEsiGroup { get; set; }
        public string OldPfEsiGroup { get; set; }
        public string NewPfEsiGroup { get; set; }
        public string OldPfEsiApplicable { get; set; }
        public string NewPfEsiApplicable { get; set; }
        public string FileName { get; set; }

    }
    public class PayrollDashBoardDiagram
    {
        public string Month { get; set; }
        public int Manpower { get; set; }
        public int GrossPay { get; set; }
    }
}

