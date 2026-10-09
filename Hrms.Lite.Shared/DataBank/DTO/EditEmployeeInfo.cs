using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EditEmployeeInfo
    {
        public Guid EmployeeGI { get; set; }
        public Guid LogGI { get; set; }
        public int EditSummaryCount { get; set; }
        public int AppPendingCount { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public EmployeeSettings EmployeeSettings { get; set; }
        public EmployeeBasicInfo EmployeeBasicInfo { get; set; }
        public StatutoryClassification StatutoryClassification { get; set; }

        public string EntryUserName { get; set; }
        public string ApproveUserName { get; set; }
        public string Remarks { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDateAndTime { get; set; }
        public DateTime ApproveDateAndTime { get; set; }
        public int EmployeeId { get; set; }
        public List<EditInfoDetailsList> EditInfoDetailsList { get; set; }
        public List<EditInfoList> EditInfoList { get; set; }
        public string ApproveMode { get; set; }
        public FilterSlabComboFill FilterCombo { get; set; }
        public Filter Filter { get; set; }
        public int TabCode { get; set; }
        public bool MakerChecker { get; set; }
        public string FirstTab { get; set; }
        public string Type { get; set; }
        public Filter filter { get; set; }
        public int mode { get; set; }

        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }

        public List<EditEmployeeInfo> EditEmpInfoList { get; set; }

        public int totalRecords { get; set; }

        public int active { get; set; }
        public bool PersonalApprovalPending { get; set; }
        public bool StatutoryApprovalPending { get; set; }
        public bool PaymentApprovalPending { get; set; }
    }
    public class EditInfoDetailsList
    {
        public string Name { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
    }
    public class EditInfoList
    {
        public Guid EmployeeGI { get; set; }
        public Guid LogGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public DateTime ExpectedDOJ { get; set; }
        public DateTime EditDate { get; set; }
        public bool MakerChecker { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int resigned { get; set; }
    }
}
