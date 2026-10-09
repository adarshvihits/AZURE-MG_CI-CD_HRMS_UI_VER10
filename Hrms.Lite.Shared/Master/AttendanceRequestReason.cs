using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class AttendanceRequestReason : MasterBase
    {
        public int? Amount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public Guid ReasonGI { get; set; }
        public int LogCode { get; set; }
        public int ReasonCode { get; set; }
        public string EntryType { get; set; }
        public bool SalaryComponent { get; set; }
        public string AllowanceType { get; set; }
        public AllowanceTypeCbo AllowanceTypeCbo { get; set; }
        public ApplicableFor ApplicableFor { get; set; }
        public List<AttendanceRequestReason> RequestReason { get; set; }
        public ResponseEntity ResponseEntity { get; set; }
        public List<Reason_SetApplicability> Reason_SetApplicability { get; set; }
        public int DisplayOrder { get; set; }
        public string ApplicableRequest { get; set; }
    }
    public class Reason_SetApplicability
    {
        public int LeaveCode { get; set; }
        public int ReasonCode { get; set; }
        public bool Applicability { get; set; }
        public string LeaveName { get; set; }

    }
}
