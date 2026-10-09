using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class VacancyManagement
    {
        public string JobCode { get; set; }

        public string Experience { get; set; }
        public string AgeGroup { get; set; }
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int Tab5Count { get; set; }
        public int Tab6Count { get; set; }
        public int Approvestatus { get; set; } // 1-Approve 2-Reject
        public string ApproverComments { get; set; }
        public List<VacancyManagement_List> VacancyManagement_List { get; set; }
        public List<VacancyManagement_List> GraphRequrements { get; set; }
        public List<VacancyManagement_List> GraphResume { get; set; }
        public List<VacancyManagement_List> GraphStatus { get; set; }
        public VacancyManagement_Details Details { get; set; }
        //public int Aging { get; set; }
        public List<VacancyManagementListTabs> VacancyManagementListTabs { get; set; }
        public bool FilledVacancy { get; set; }
        public int NoOfRecruitments { get; set; }
        public bool JDCheck { get; set; }
        public List<EmployeeDT> EmployeeDtList { get; set; }


    }
    public class VacancyManagement_List
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
        public string ExGrossPay { get; set; }
        public string FullName { get; set; }

    }
    public class VacancyManagementListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class EmployeeDT
    {
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public int EmployeeCode { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
        public Gender Gender { get; set; }
        public DateTime NoticeGivenDate { get; set; }
        public DateTime LWD { get; set; }
        public string Reason { get; set; }
        public string Replacement { get; set; }
        public int?[] GenderArray { get; set; }
        public File Attachment { get; set; }
        public int Age { get; set; }
        public string YOS { get; set; }
        public int GrossPay { get; set; }
        public WorkLocation WorkLocation { get; set; }
    }
    public class VacancyManagement_Details : EmployeeDT
    {
        public string JobCode { get; set; }

        public string JobTitle { get; set; }

        public string Description { get; set; }
        public DateTime? NeedByDate { get; set; }
        public int? NoOfRequirements { get; set; }
        public string ExGrossPay { get; set; }
        public string RequestedEmployee { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime ClosedOrCancelledDate { get; set; }
    }


}
