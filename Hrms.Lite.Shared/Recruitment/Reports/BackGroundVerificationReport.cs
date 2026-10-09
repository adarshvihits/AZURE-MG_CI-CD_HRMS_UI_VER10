using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Recruitment.Reports
{
    public class BackGroundVerificationReport
    {
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string RefNo { get; set; }
        public DateTime NeedByDate { get; set; }
        public string LocationName { get; set; }
        public string CandidateName { get; set; }
        public string CandidateCode { get; set; }
        public int CandidateID { get; set; }
        public string EmployeeID { get; set; }
        public string CandidatePhoto { get; set; }
        public int DaysLeft { get; set; }
        public int Age { get; set; }
        public string Experience { get; set; }
        public bool Aadhar { get; set; }
        public string EntryUser { get; set; }
        public Designation Designation { get; set; }
        public int AddressStatus { get; set; }
        public int EducationStatus { get; set; }
        public int Employment_BackgroundStatus { get; set; }
        public int Relieving_LetterStatus { get; set; }
        public List<BackGroundVerificationReport> BackGroundVerificationReport_List { get; set; }
    }
}
