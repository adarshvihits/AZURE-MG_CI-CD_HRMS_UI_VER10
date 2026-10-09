using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class LeaveRule
    {

        public bool Leave_Appl { get; set; }
        public int OpeningBased { get; set; }
        public int FixedOrProDataBase { get; set; }
        public decimal MaxOpening { get; set; }
        public int LeaveCreditFreq { get; set; }
        public decimal LeaveCreditCount { get; set; }
        public decimal MaxLeaveHold { get; set; }
        public bool Encashment_Appl { get; set; }
        public bool LeavelapsAppl { get; set; }
        public decimal MandatoryTakenlaps { get; set; }
        public decimal EncashmentMaxLimit { get; set; }
        public bool CarryForward_Appl { get; set; }
        public decimal CarryForwardMaxLimit { get; set; }
        public bool HalfDay_Appl { get; set; }
        public decimal? NoOfLeavesAtaTime { get; set; }
        public int? NoOfLeaveApplPerYr { get; set; }
        public bool PreDatedApplAlwed { get; set; }
        public int? NoOfDaysPrior { get; set; }
        public bool AdvanceApplRequired { get; set; }
        public int? NoOfDaysAdvance { get; set; }
        public bool ApplyProDataBasis { get; set; }
        public bool RemarksMandatory { get; set; }
        public bool WrkHandOvrMandatory { get; set; }
        public bool Mandatory { get; set; }
        public decimal? NoOfLeavesForMandatoryDoc { get; set; }
        public bool ClubWithOtherLeaves { get; set; }
        public bool HolidaySandwichAppl { get; set; }
        public int? HolidaySandwitchNoOfLeaves { get; set; }
        public bool OffdaySandwichAppl { get; set; }
        public int? OffdaySandwitchNoOfLeaves { get; set; }
        public Leave Leave { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public decimal? ProDataBasisNoOfLeaves { get; set; }
        public decimal? NoofLeavesPerMonths { get; set; }
        public bool OnboardingApplicability { get; set; }
        public bool NoticePeriodApplicability { get; set; }
        public bool JoiningMonthApplicability { get; set; }
        public int[] ClubbingLeaves { get; set; }
        public bool AdvleaveApplNecessary { get; set; }
        public int? AdvleaveApplNecessaryDays { get; set; }

        public decimal? MinNoOfLeaveAtaTime { get; set; }
        public bool HolidayCompoundLeave { get; set; }
        public bool OffdayCompoundLeave { get; set; }
    }
}
