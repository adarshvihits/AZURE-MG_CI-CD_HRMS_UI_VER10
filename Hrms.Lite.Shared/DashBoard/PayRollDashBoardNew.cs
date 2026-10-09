using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class PayRollDashBoardNew
    {
        public int FandFPendingCount { get; set; }
        public int SalaryVariance { get; set; }
        public int LoanBalance { get; set; }
        public int AccNoVariance { get; set; }
        public int SalaryHold { get; set; }
        public int PFApplicable { get; set; }
        public int PFNotApplicable { get; set; }
        public int PFVariance { get; set; }
        public int ESIApplicable { get; set; }
        public int ESINotApplicable { get; set; }
        public int ESIVariance { get; set; }
        public int LoanConfirmation { get; set; }
        public int TDSOld { get; set; }
        public int TDSNew { get; set; }
        public int ActivePayrollEmployee { get; set; }
        public string ApplicableType { get; set; }
        public string ApplicableSubType { get; set; }
  
        public List<DashBoardNotification> DashBoardNotificationList { get; set; }
        public List<PayrollDashBoardNewList> PayrollDashBoardList { get; set; }
        public List<ApplicableCount> ApplicableCountList { get; set; }
        public List<ApplicablePopupList> ApplicableList { get; set; }
        public List<ApplicablePopupList> OTHER_Variance_Applicable { get; set; }
        public List<ApplicablePopupList> ESI_PF_PT_Variance_Applicable { get; set; }
        public List<Graph> GraphList { get; set; }
    }
    public class PayrollDashBoardNewList
    {
        public int EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string PayrollGroup { get; set; }
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
        public string PfEsiGroup { get; set; }
        public string OldPfEsiGroup { get; set; }
        public string NewPfEsiGroup { get; set; }
        public string OldPfEsiApplicable { get; set; }
        public string NewPfEsiApplicable { get; set; }
        public string Count { get; set; }
        public string Age { get; set; }
        public string EmployeeStatus { get; set; }

    }


    public class ApplicableCount
    {
        public int SLNO { get; set; }
        public string TYPE { get; set; }
        public int YES { get; set; }
        public int NO { get; set; }
        public int CHANGE { get; set; }

    }
    public class ApplicablePopupList
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
        public string GroupName { get; set; }
        public string OldGroupName { get; set; }
        public string NewGroupName { get; set; }
        public string OldApplicable { get; set; }
        public string NewApplicable { get; set; }
        public string Count { get; set; }
    }
    public class Graph
    {
        public int SalaryPeriodCode { get; set; }
        public string SalaryPeriodName { get; set; }
        public decimal Gross { get; set; }
        public decimal NET { get; set; }
        public decimal CTC { get; set; }

    }
}
