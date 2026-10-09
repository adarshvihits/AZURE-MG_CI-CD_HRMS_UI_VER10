using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class SalaryPosting
    {
        public int count { get; set; }
        public MasterBase PayrollGroup { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public MasterBase Employee { get; set; }
        public List<PostingVerification> PostingVerification { get; set; }
        public List<PostingDetails> PostingDetails { get; set; }
        public List<PostingEmpList_Popup> PostingEmpList_Popup { get; set; }
        public List<PostingValidation_Popup> PostingValidation_Popup { get; set; }
        public string Heading { get; set; }
        public List<SalaryReconciliationPostingDetails> SalaryReconciliationPostingDetails { get; set; }
        public List<SalaryPostList> SalaryPostList { get; set; }
        public Filter Filter { get; set; }
    }

    public class PostingVerification
    {

        public int Code { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }

    public class PostingDetails
    {

        public int ID { get; set; }
        public int Code { get; set; }
        public string TYPE { get; set; }
        public string Description { get; set; }
        public int EmployeeCount { get; set; }
        public int Amount { get; set; }
        public bool Link { get; set; }
    }

    public class PostingEmpList_Popup
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public decimal Amount { get; set; }
    }
    public class PostingValidation_Popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public int LOP { get; set; }
        public DateTime ExDOJ { get; set; }
        public string Bank { get; set; }
        public string AccNo { get; set; }
        public string IFSC { get; set; }
        public string PrevAccNo { get; set; }
        public string CurrAccNo { get; set; }
        public string PrevPayrollGroup { get; set; }
        public string CurrPayrollGroup { get; set; }
        public string PFApplicable { get; set; }
        public string OldPFGroup { get; set; }
        public string NewPFGroup { get; set; }
        public string PrevPFApplicability { get; set; }
        public string CurrPFApplicability { get; set; }
        public string GrossSalary { get; set; }
        public string OldEsiGroup { get; set; }
        public string NewEsiGroup { get; set; }
        public string PrevEsiApplicability { get; set; }
        public string CurrEsiApplicability { get; set; }
        public string Stage { get; set; }
        public string JobTransitionType { get; set; }
        public string Remarks { get; set; }
    }
    public class SalaryReconciliationPostingDetails
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public int PreviousEmpCount { get; set; }
        public int PreviousGross { get; set; }
        public int PreviousEarning { get; set; }
        public int CurrentEmpCount { get; set; }
        public int CurrentGross { get; set; }
        public int CurrentEarning { get; set; }
        public int Variance { get; set; }
        public int Remark { get; set; }
        public string Type { get; set; }
        public int EmpCount { get; set; }
        public int Salary { get; set; }
        public int BankCode { get; set; }
        public bool Link { get; set; }
    }
    public class SalaryPostList
    {

        public EmployeeHeader EmployeeHeader { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal LOPDays { get; set; }
        public decimal PaidDays { get; set; }
        public decimal ActualGross { get; set; }
        public decimal LOPAmount { get; set; }
        public decimal EarnedGross { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal HoldAmount { get; set; }
        public decimal NetPay { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}

