using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class WorkUnitTransferRequest
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int CurrentWorkUnitCode { get; set; }
        public string CurrentWorkUnitName { get; set; }
        public int NewWorkUnitCode { get; set; }
        public string NewWorkUnitName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Remarks { get; set; }
        public string ApproverRemarks { get; set; }
        public string Mode { get; set; }
        public List<WorkUnitAuhtority> WorkUnitAuthorityList { get; set; }
        public List<WorkUnitEmployeeApprovalList> ApprovalList { get; set; }
    }
    public class WorkUnitAuhtority
    {
        public int RequestTypeCode { get; set; }
        public string RequestTypeName { get; set; }
        public int LevelCode { get; set; }
        public string LevelName { get; set; }
        public int AuthorityCode { get; set; }
        public string AuthorityName { get; set; }
    }
    public class WorkUnitEmployeeApprovalList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string RequestedEmployee { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int OldWorkUnitCode { get; set; }
        public string OldWorkUnitName { get; set; }
        public int NewWorkUnitCode { get; set; }
        public string NewWorkUnitName { get; set; }       
    }
}
