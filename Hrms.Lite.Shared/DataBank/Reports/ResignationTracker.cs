using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ResignationTracker
    {      
        public List<ResignationTrackerList> List { get; set; }
        public ResignationTrackerList Details { get; set; }
        public List<ResignationTracker_Data> Tracker { get; set; }
        public Filter Filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<ApprovalTracker> TrackerDetails { get; set; }
        public List<EmployeeResignationAcceptanceList> AcceptanceList { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Status { get; set; }
        public File File { get; set; }
    }

    public class EmployeeResignationAcceptanceList
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
        public string? ApprovalStatus { get; set; }
        public string Reason { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public int ResignationCancelCode { get; set; }
        public Guid ResignationCancelGI { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }


    }
    public class ApprovalTracker
    {
        public string Stage { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }
        public string Approver { get; set; }
    }
    public class ResignationTrackerList
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public DateTime DOJ { get; set; }
        public string YearOfService { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public string Stage { get; set; }
        public string Phone { get; set; }
        public string MailID { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public DateTime? ApprovedLWD { get; set; }
        public string Justification { get; set; }


    }
    public class ResignationTracker_Data
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public string Stage { get; set; }
        public string AuthorityName { get; set; }
        public string AuthorityLevel { get; set; }
        public DateTime? EntryDate { get; set; }
        public string ResignationStatus { get; set; }
        public string FinalStatus { get; set; }


    }


}
