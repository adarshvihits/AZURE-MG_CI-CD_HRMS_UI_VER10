using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class RecruitmentDashBoard
    {
      
            public int ActiveVacancy { get; set; }
            public int OverDueVacancy { get; set; }
            public int CriticalVacancy { get; set; }
        public int InProgress { get; set; }
        public int PostedMRF { get; set; }
            public int PendingForPost { get; set; }
            public int ShortListed { get; set; }
            public int ShortListedInterview { get; set; }
            public decimal OfferAccpRatio { get; set; }
            public int Offeraccepted { get; set; }
            public int OfferRejected { get; set; }
            public int VacancyClosedThisWeek { get; set; }
            public int FinalApproval { get; set; }
            public int OfferProcessing { get; set; }
            public int OfferVerification { get; set; }
            public int EmploymentChecking { get; set; }
            public int OfferInitialize { get; set; }
            public int CreditChecking { get; set; }
            public  Dropdown ClassificationType { get; set; }
            public Dropdown Classification { get; set; }
        public List<RecruitmentDashBoardChartList> ChartList { get; set; }
        public List<RecruitmentDashBoardChartList> BarChartList { get; set; }
        public List<RecruitmentDashBoardChartList> FunnelChartList { get; set; }
        public List<VacancyNearlyDueList> VacancyNearlyDueList { get; set; }
        public int CancelledVacancy { get; set; }
    }

    public class RecruitmentDashBoardChartList
    {

        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class VacancyNearlyDueList
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public int NoOfRequirements { get; set; }
        public string RequestedBy { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string Gender { get; set; }
        public string Experience { get; set; }
        public int Aging { get; set; }
        public string RecruitmentComments { get; set; }
        public int MappedResumes { get; set; }
        public string VacancyType { get; set; }
        public string AgeGroup { get; set; }
        public string RefNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime ClosedOrCancelledDate { get; set; }
        public string MRFCreatedDate { get; set; }
        public string MRFApprovedDate { get; set; }
        public string MRFCanceledDate { get; set; }
        public string MRFClosedDate { get; set; }

    }


}
