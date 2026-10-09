using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class Resignation
    {
        public int EmpId { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid ResignationGI { get; set; }
     
        public EmployeeHeader EmployeeHeader { get; set; }
        public int Count_InNoticePeriod { get; set; }
        public int Count_ResignationAccept { get; set; }
        public int Count_ExitChecklistPending { get; set; }
        public int Count_RelivingPending { get; set; }
        public int Count_RetirementDueList { get; set; }
        public int Count_ResignationApproval { get; set; }
        public int Count_ResignationAmendment { get; set; }
        public ResignationList ResignationDeleteDetails { get; set; }
        public List<ResignationList> ResignationList { get; set; }
        public List<ResignationListTabs> ResignationListTabs { get; set; }
        public ResignationDetails ResignationDetails { get; set; }
        public List<ApprovalPendingDetails> ApprovalPendingDetails { get; set; }
        public List<NoticePeriodDetails> NoticePeriodDetails { get; set; }
        public List<ExitCheckDetails> ExitCheckList { get; set; }
        public List<DueClearanceSettings> DueClearanceSettings { get; set; }
        public List<DueClearanceAuthority> DueClearanceAuthority { get; set; }
        public ResignationList ResignationAmendmentDetails { get; set; }
        public string ApproveMode { get; set; }
        public FilterSlabComboFill Filter { get; set; }
        public Filter SliderFilter { get; set; }
        public int TabCode { get; set; }
        public List<FormPermissionMenuTab> UserVsMenuTab { get; set; }
        public string FirstTab { get; set; }
      
    }
public class DueClearanceAuthority
{
    public int EmployeeCode { get; set; }
    public int ParameterCode { get; set; }
}
public class ResignationList
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Filter SliderFilter { get; set; }
        public DateTime? DOJ { get; set; }
        public string YOS { get; set; }
        public DateTime? LWD { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string Reason { get; set; }
        public string Stage { get; set; }
        public string Remarks { get; set; }
        public string DueStatus { get; set; }
        public string AuthName { get; set; }
        public string AmendmentRemark { get; set; }
        public DateTime? ReportingMangerLWD { get; set; }
        public DateTime? SkipLevelMangerLWD { get; set; }
        public int Level { get; set; }
        public string Authority1 { get; set; }
        public string Authority2 { get; set; }

        public string Status { get; set; }

    }
    public class ResignationListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class ResignationDetails
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public ResignationType ResignationType { get; set; }
        public ResignationReason ResignationReason { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public bool Rehire { get; set; }
        public File File { get; set; }
        public string Remarks { get; set; }
        public string AuthorityRemarks { get; set; }
        
        public string AcceptanceRemarks { get; set; }
        public string WorkHandOverDetails { get; set; }
        public string RelievingRemarks { get; set; }
        public int IsReplacement { get; set; }
        public bool SkipDuesClearance { get; set; }
        public DateTime? LastPunchingDate { get; set; }
        public DateTime? LastPresentDate { get; set; }
        public string PunchTime { get; set; }

    }
    public class ExitCheckDetails
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public int SlNo { get; set; }
        public int CheckTypeCode { get; set; }
        public string CheckType { get; set; }
        public int CheckItemCode { get; set; }
        public string CheckItem { get; set; }
        public ExitCheckStatus ExitCheckStatus { get; set; }
        public string Remarks { get; set; }
        public bool IsMandatory { get; set; }
    }
    public class ApprovalPendingDetails
    {
        public string Description { get; set; }
        public string PendingCount { get; set; }
    }
    public class NoticePeriodDetails
    {
        public string Description { get; set; }
        public string InDays { get; set; }
    }

    public class ExitCheckStatus : MasterBase
    {

    }
}

