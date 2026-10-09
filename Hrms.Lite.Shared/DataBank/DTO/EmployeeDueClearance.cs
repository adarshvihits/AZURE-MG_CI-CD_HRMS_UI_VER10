using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeDueClearance
    {       
        public EmployeeDueClearance_EmployeeDetails EmployeeDetails { get; set; }
        public EmployeeDueClearance_Details Details { get; set; }
        public List<EmployeeDueClearance_Details> ParameterDetails { get; set; }
        public List<EmployeeDueClearance_EmployeeDetails> List { get; set; }
        public Filter Filter { get; set; }
    }
    public class EmployeeDueClearance_EmployeeDetails
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public DateTime? DOJ { get; set; }
        public string YOS { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LastWorkingDateAsperPolicy { get; set; }
        public DateTime? RequestedLastWorkingDate { get; set; }
        public DateTime? ClearanceDate { get; set; }
        public bool Status { get; set; }
        public int PendingCount { get; set; }
    }
    public class EmployeeDueClearance_Details
    {
        public int SlNo { get; set; }
        public Guid ResignationGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid ParameterGI { get; set; }
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public Department Department { get; set; }
        public MasterBase DuesStatus { get; set; }
        public int CostDeduction { get; set; }
        public string Remarks { get; set; }
        public string AuthName { get; set; }
        public bool Status { get; set; }
    }
}
