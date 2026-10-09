using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class VacancyManagementApproval
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string VacancyType { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedBy { get; set; }
        public DateTime NeedByDate { get; set; }
        public int NoOfRequirements { get; set; }
        public int Aging { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public string Experience { get; set; }
        public string ExGrossPay { get; set; }
        public string AgeGroup { get; set; }
        public string Remarks { get; set; }
        public int Approvestatus { get; set; } // 1-Approve 2-Reject
        public string ApproverComments { get; set; }
        public List<VacancyManagementApproval> VacancyManagementApprovalList { get; set; }
    }
}
