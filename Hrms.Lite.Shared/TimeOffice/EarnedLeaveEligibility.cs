using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class EarnedLeaveEligibility
    {
        public UserBase User { get; set; }
        public Month Month { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public File Excel { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public int ConfirmationStatus { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public DateTime DOJ { get; set; }
        public int ELOpening { get; set; }
        public string ELOpeningString { get; set; }
        
        public int ABLOP { get; set; }
        public int PayDays { get; set; }
        public List<EarnedLeaveEligibility> EarnedLeaveEligibilityList { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string TabIndex { get; set; }
        public int TabCode { get; set; }
        public int Count_NotConfirmed { get; set; }
        public int Count_Confirmed { get; set; }
        public string Remarks { get; set; }
    }
}
