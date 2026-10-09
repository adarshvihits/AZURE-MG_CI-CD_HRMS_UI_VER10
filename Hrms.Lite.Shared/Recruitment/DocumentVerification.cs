using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Recruitment
{
    public class DocumentVerification:DocumentUpload
    {
        public int ApplicantTrackingCode { get; set; }
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public Location Location { get; set; }
        public string CandidateName { get; set; }
        public string CandidateCode { get; set; }
        public int CandidateID { get; set; }
        public string CandidatePhoto { get; set; }
        public int DaysLeft { get; set; }
        public int Age { get; set; }
        public string Experience { get; set; }
        public bool Aadhar { get; set; }
        public bool PAN { get; set; }
        public bool DrivingLicense { get; set; }
        public bool Passport { get; set; }
        public bool Others { get; set; }
        public Designation Designation { get; set; }
        public string AadharUrl { get; set; }
        public string PANUrl { get; set; }
        public string DrivingLicenseUrl { get; set; }
        public string DLUrl { get; set; }
        public string PassportUrl { get; set; }

        public string OtherDocName { get; set; }
        public string OthersUrl { get; set; }

        public int Verficationstatus { get; set; }
        public List<DocumentVerification> Document_List { get; set; }
        public List<OtherDocList> OtherDocList { get; set; }
        public string DocType { get; set; }

        public File File { get; set; }

    }
}
