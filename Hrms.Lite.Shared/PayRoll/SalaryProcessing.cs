using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SalaryProcessing
    {      
        public PayrollMonth PayrollMonth { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public MasterBase Employee { get; set; }
        public DateTime LWD { get; set; }
        public List<SalaryProcessing_Details> HR_Time_Office { get; set; }
        public List<SalaryProcessing_Details> Statutory { get; set; }
        public List<SalaryProcessing_Details> Payroll_Settings { get; set; }
        public List<SalaryProcessing_Details> Payroll_Transaction { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int EmployeeStatus { get; set; }
        public SalaryProcessing_Validation_Msg Validation_Msg { get; set; }
        public SalaryIntegrityReport SalaryIntegrityReport { get; set; }
        public List<SalaryProcessing_popup> SalaryProcessing_popup { get; set; }
    }
    public class SalaryProcessing_Details
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
    public class SalaryProcessing_Validation_Msg
    {
        public string Description { get; set; }
        public int FLAG { get; set; }
    }





    public class SalaryProcessing_popup
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
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NET_SALARY { get; set; }
        public string Remarks { get; set; }
    }


}