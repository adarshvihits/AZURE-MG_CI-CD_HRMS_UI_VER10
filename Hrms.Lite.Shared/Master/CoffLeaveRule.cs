using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class CoffLeaveRule
    {

        public LeaveGroup LeaveGroup { get; set; }
        public string HoursToWork { get; set; }
        public bool OffdayApplicable { get; set; }
        public bool HolidayApplicable { get; set; }
        public int ExpiryPeriod { get; set; }
        public bool CoffCreditApprovalRequired { get; set; }
        public bool HRMSApprovalRequired { get; set; }
        public bool ESSPApprovalRequired { get; set; }
        public bool PublicHolidayApplicable { get; set; }
        public bool FestivalHolidayApplicable { get; set; }
        public bool RestrictedHolidayApplicable { get; set; }
        public bool ClubWithOtherLeaves { get; set; }
        public int[] ClubbingLeaves { get; set; }
    }
}
