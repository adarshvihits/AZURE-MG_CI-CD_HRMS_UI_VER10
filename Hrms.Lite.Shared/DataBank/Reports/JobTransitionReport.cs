using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class JobTransitionReport
    {
     
        public int TotalRows { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public JobTransitionsType JobTransitionsType { get; set; }
        public List<dynamic> Data { get; set; }
        public JobTransitionReport_MainList MainList { get; set; }
        public List<JobTransitionReport_GridSettings> GridSettings { get; set; }
        public Filter filter { get; set; }
        public List<JobTransitionReportDetails> JobTransitionReportDetails { get; set; }
    }
    public class JobTransitionReport_GridSettings
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }
    }
    public class JobTransitionReport_MainList
    {
        public List<JobTransitionReport_Header> Header { get; set; }
        public List<JobTransitionReport_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public List<JobTransitionReport_Details> JobTransitionReport_Details { get; set; }
    }
    public class JobTransitionReport_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class JobTransitionReport_EmployeePrimaryInfo
    {
        public Guid HistoryGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransitionType { get; set; }
        public string Reason { get; set; }

    }
    public class JobTransitionReport_Details
    {
        public string Description { get; set; }

    }
    public class JobTransitionReportDetails
    {
        public Guid HistoryGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string TransitionType { get; set; }
        public string Reason { get; set; }
        public string BasicSalary { get; set; }
        public string OtherAllowance { get; set; }
        public string GrossSalary { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Grade { get; set; }
        public string PayrollGroup { get; set; }
        public string PFApplicability { get; set; }
        public string ESIApplicability { get; set; }
        public string GrossPay { get; set; }
        public string PreDesignation { get; set; }
        public string PreDepartment { get; set; }
        public string PreLocation { get; set; }
        public string PreCategory { get; set; }
        public string PreGrade { get; set; }
        public string PrePayrollGroup { get; set; }
        public string PrePFApplicability { get; set; }
        public string PreESIApplicability { get; set; }
        public string PreGrossPay { get; set; }

    }
}
