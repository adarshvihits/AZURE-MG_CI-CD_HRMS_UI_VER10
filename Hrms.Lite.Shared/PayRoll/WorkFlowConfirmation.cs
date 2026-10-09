using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class WorkFlowConfirmation
    {
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public string Location { get; set; }
        public int EmployeeCode { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown Allowance { get; set; }
        public Dropdown PGroup { get; set; }
        public Filter Filter { get; set; }
        public string TabValues { get; set; }
        public int ConfirmationStatus { get; set; }
        public Guid EmployeeGI { get; set; }
        public Dropdown PayrollGroup { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public string IDName { get; set; }
 
        public string Tab { get; set; }
        public int MonthCode { get; set; }
        public decimal DeductionDays { get; set; }
        public decimal TotalMinToCutEditDays { get; set; }
        public string Remarks { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string AllowanceName { get; set; }
        public string Status { get; set; }
        public Decimal ApprovedAmount { get; set; }
        public Decimal FinalAmount { get; set; }
        public List<WorkFlowConfirmation> WorkFlowConfirmationList { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public Decimal TotalSum { get; set; }
    }
}
