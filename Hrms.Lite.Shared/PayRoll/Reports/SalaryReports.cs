using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class SalaryReports
    {

       
        public MasterBase PayrollMonth { get; set; }
        public List<SalarySummary_EmployeeList> SalarySummary { get; set; }
        public List<SalarySummary_GroupSummary> GroupSummary { get; set; }
        public List<MasterBase> Allowances { get; set; }
        public List<MasterBase> Deductions { get; set; }
        public List<dynamic> Data { get; set; }
        public SalaryReports_MainList MainList { get; set; }
        public GroupByComboFill GroupByFill { get; set; }
        public string groupbyname { get; set; }
        public Filter Filter { get; set; }
        public bool AllApplicable { get; set; }
        public string allowanceIds { get; set; }
        public string deductionIds { get; set; }
        public List<AlwSettings> AlwSettings { get; set; }
        public List<DedSettings> DedSettings { get; set; }
        public int Total_GrossAllowance { get; set; }
        public int Total_GrossDeduction { get; set; }
        public int Total_Advance { get; set; }
        public int Total_NetPay { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int GroupCompanyId { get; set; }
        public Url Url { get; set; }

    }

    public class AlwSettings
    {
        public int AllowanceCode { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }

    }

    public class DedSettings
    {
        public int DeductionCode { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }

    }


    public class SalarySummary_EmployeeList
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

        public string PayrollGroup { get; set; }
        public int LOP { get; set; }
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NetPay { get; set; }
        public string PaymentMode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class SalarySummary_GroupSummary
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Count_Perc { get; set; }
        public int GrossAlw { get; set; }
        public decimal GrossAlw_Perc { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NetPay { get; set; }
    }
    public class SalaryReports_MainList
    {
        public List<SalaryReports_Header> Header { get; set; }
        public List<SalaryReports_Info> SalaryReports_Info { get; set; }
        public List<SalaryReports_Details> SalaryReports_Details { get; set; }


    }
    public class SalaryReports_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class SalaryReports_Info
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string PFNumber { get; set; }
        public string PaymentMode { get; set; }
        public decimal PresentDays { get; set; }
        public decimal LOP { get; set; }

    }
    public class SalaryReports_Details
    {
        public decimal Amount { get; set; }

    }
    public class SalaryIntegrityReport
    {
        public UserBase User { get; set; }
        public Filter Filter { get; set; }
        public MasterBase MonthA { get; set; }
        public MasterBase MonthB { get; set; }
        public int Allowance_Deduction { get; set; }
        public MasterBase Type { get; set; }
        public int CheckList_Status { get; set; }
        public int ExcludeZeroDifference { get; set; }
        public int ShowNegativeDifference { get; set; }
        public int Difference_ex_shw { get; set; }
        public int DiffFrom { get; set; }
        public int DiffTo { get; set; }
        public List<SalaryIntegrityReport_CheckList> CheckList { get; set; }
        public List<SalaryIntegrityReportList> MainList { get; set; }
        public List<SalaryIntegrityReport_Details> Details { get; set; }
    }
    public class SalaryIntegrityReport_CheckList
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }

    public class SalaryIntegrityReportList
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Category { get; set; }
        public decimal MonthA_Amount { get; set; }
        public decimal MonthB_Amount { get; set; }
        public decimal Difference { get; set; }
        public decimal MonthA_LOP { get; set; }
        public decimal MonthB_LOP { get; set; }
        public int totalRecords { get; set; }
        public int Active { get; set; }

        public decimal MonthA_ActualGross { get; set; }
        public decimal MonthA_EarnedGross { get; set; }
        public decimal MonthB_ActualGross { get; set; }
        public decimal MonthB_EarnedGross { get; set; }

    }
    public class SalaryIntegrityReport_Details
    {
        public string TYPE { get; set; }
        public int CODE { get; set; }
        public string Name { get; set; }
        public decimal AmountA { get; set; }
        public decimal AmountB { get; set; }
        public decimal Difference { get; set; }
        public decimal LopA { get; set; }
        public decimal LopB { get; set; }
    }
}

