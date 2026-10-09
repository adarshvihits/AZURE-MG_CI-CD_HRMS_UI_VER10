using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class LeaveOpening
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public DateTime DOJ { get; set; }
        public decimal Opening { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public LeaveOpeningBatchUpDate LeaveOpeningBatchUpDate { get; set; }
        public LeaveOpeningEdit LeaveOpeningEdit { get; set; }
        public List<LeaveOpening> LeaveOpeningList { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string UserName { get; set; }
        public string EntryFrom { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Applicability { get; set; }
        public bool AllLeaveopening { get; set; }
        public int EntryType { get; set; }
        public string OpeningStr { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
    }
    public class LeaveOpeningBatchUpDate
    {
        public decimal Opening { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
    }
    public class LeaveOpeningEdit
    {
        public Location Location { get; set; }
        public DateTime DOJ { get; set; }
        public decimal Opening { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public decimal OpeningEdit { get; set; }
    }

}
