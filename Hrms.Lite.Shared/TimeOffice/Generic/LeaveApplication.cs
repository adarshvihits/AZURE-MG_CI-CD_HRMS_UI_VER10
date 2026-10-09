using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Generic
{
    public class LeaveApplication
    {
       public List<LeaveBalance> LeaveBalanceList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public LeaveBalance leaveBalance { get; set; }


        public DateTime DateOfApplication { get; set; }
        public MasterBase LeaveType { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime? From { get; set; }
        public MasterBase FromType { get; set; }
        public DateTime? To { get; set; }
        public MasterBase ToType { get; set; }
        public MasterBase LeaveReason { get; set; }
        public string AdditionalRemarks { get; set; }
        public string HandOverDetails { get; set; }
        public string ContactDetails { get; set; }
        public LeaveDetails LeaveDetails { get; set; }
        public File Attachment { get; set; }
        public List<LeaveApplication> LeaveApplicationList { get; set; }
        public List<Tracker> Tracker { get; set; }
        public Guid LeaveApplicationGI { get; set; }
        public string Status { get; set; }
        public string TotalDays { get; set; }
        public string NoOfDays { get; set; }

        public int ApproveCount { get; set; }
        public int RejectedCount { get; set; }
        public int PendingCount { get; set; }
        public int TotalCount { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public MasterBase LeavePeriod { get; set; }
        public Filter filter { get; set; }
        public List<EmployeeAuthority> EmployeeAuthority { get; set; }
        public string Breadcrumb { get; set; }
        public string BreadcrumbName { get; set; }
        public int AttndanceLeaveType { get; set; }
        public Guid EmployeeGI { get; set; }
        public List<LeaveApplicationEmployeeList> LeaveApplicationEmployeeList { get; set; }


        
    }
    public class LeaveApplicationEmployeeList
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
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

    public class LeaveBalance
    {
        public List<LeaveBalance> LeaveBalanceList { get; set; }

        public string LeaveType { get; set; }
        public string ShortName { get; set; }
        public decimal Openings { get; set; }
        public decimal CarryForward { get; set; }
        public decimal Increment { get; set; }
        public decimal Decrement { get; set; }
        public decimal Taken { get; set; }
        public decimal Balance { get; set; }
        public decimal Applied { get; set; }
        public int? AttendaceStatusCode { get; set; }

        public MasterBase LeavePeriod { get; set; }
    }
    public class EmployeeAuthority
    {
        public string Level { get; set; }
        public string AuthorityName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AbsoluteUri { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }

        public List<EmployeeAuthority> EmployeeAuthorityList { get; set; }

        public string FileName { get; set; }
        public string Base64string { get; set; }

    }


    public class LeaveDetails
    {
        public decimal LeaveDays { get; set; }
        public decimal WorkingDays { get; set; }
        public decimal Holidays { get; set; }
        public decimal OffDays { get; set; }
        public List<LeaveDayWiseDetails> DayWiseDetails { get; set; }
        public List<EmployeeAuthority> EmployeeAuthority { get; set; }

    }
    public class LeaveDayWiseDetails
    {
        public DateTime LeaveDate { get; set; }
        public string Day { get; set; }
        public MasterBase Session { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
    }
}
