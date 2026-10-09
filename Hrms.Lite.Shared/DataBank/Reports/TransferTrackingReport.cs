using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class TransferTrackingReport
    {
        public Filter Filter { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Region { get; set; }
        public string NewFuncDesignation { get; set; }
        public string NewDepartment { get; set; }
        public string NewLocation { get; set; }
        public string NewGroupCompany { get; set; }
        public string Status { get; set; }
        public string Authority { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<TransferTrackingReport> TransferTrackingReportList { get; set; }
        public int HDCode { get; set; }
        public string RequestID { get; set; }
        public string NewFunction { get; set; }
        public string NewSubDepartment { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<EmpTransferDetails> EmpTransferDetailsList { get; set; }
    }
    public class EmpTransferDetails
    {
        public string EmployeeID { get; set; }
        public string Authority { get; set; }
        public string Designation { get; set; }
        public string Level { get; set; }
        public string Action { get; set; }
        public DateTime? ActionTime { get; set; }
    }
}
