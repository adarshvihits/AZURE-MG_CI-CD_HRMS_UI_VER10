using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class VacancyCancellation
    {
        public List<VacancyStatusReport> VacancyStatusReport { get; set; }
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
        public MasterBase CancellationReason { get; set; }
        public string ApproverComments { get; set; }
    }
}
