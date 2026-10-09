using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class GratuityProcessing
    {
     
        public List<EmployeeVsBreakingService> BreakingService { get; set; }
        public List<GratuityProcessing_EmployeeList> EmployeeList { get; set; }
        public GratuityProcessing_EmployeeList EmployeeData { get; set; }
        public EmployeeVsGratuity EmployeeGaratuity { get; set; }
        public GratuityPayment GratuityPayment { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public Filter Filter { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ProcessingStage { get; set; }
        public List<EmployeeGratuityReport> GratuityReport { get; set; }
    }


    public class GratuityProcessing_EmployeeList
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime? LWD { get; set; }
        public DateTime? DOC { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOB { get; set; }
        public string ResignationReason { get; set; }
        public string YearsOfService { get; set; }
        public string Aging { get; set; }
        public string Stage { get; set; }
        public int totalRecords { get; set; }
        public int Active { get; set; }
        public string Remarks { get; set; }

    }


    public class EmployeeVsBreakingService
    {
        public int BreakingServiceCode { get; set; }
        public Guid BreakingServiceGI { get; set; }
        public int EmpCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public int TotalDays { get; set; }
    }
    public class EmployeeVsGratuity
    {
        public int EmployeeCode { get; set; }
        public string BreakingYears { get; set; }
        public decimal GratuityApplSalary { get; set; }
        public decimal GratuityAmount { get; set; }
        public string ActualYears { get; set; }
        public string LIC { get; set; }
        public string PF { get; set; }
        public string UAN { get; set; }
        public string ESI { get; set; }
        public string AadharNo { get; set; }
    }
    public class GratuityPayment
    {
        public int EmployeeCode { get; set; }
        public string CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string HandoverTo { get; set; }
        public DateTime? HandoverDate { get; set; }
    }
    public class EmployeeGratuityReport
    {
        public GratuityProcessing_EmployeeList GratuityEmpDetails { get; set; }
        public EmployeeVsGratuity EmployeeGratuity { get; set; }
        public GratuityPayment GratuityPayments { get; set; }
    }
}
