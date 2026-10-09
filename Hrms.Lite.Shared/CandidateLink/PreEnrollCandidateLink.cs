using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.CandidateLink
{
    public class PreEnrollCandidateLink
    {

        public Guid PreEnrollGI { get; set; }
        public String EmployeeName { get; set; }
        public String Designation { get; set; }
        public String Department { get; set; }
        public String Location { get; set; }
        public DateTime DOJ { get; set; }
        public bool Addr { get; set; }
        public bool Cont { get; set; }
        public bool Pers { get; set; }
        public bool Fmly { get; set; }
        public bool Educ { get; set; }
        public bool Skills { get; set; }
        public bool Cert { get; set; }
        public bool Prev { get; set; }
        public bool Photo { get; set; }
        public bool EmployeeVsNominee { get; set; }
        public bool Bank { get; set; }
        public bool Doc { get; set; }
        public bool PoliceVerification { get; set; }
        public bool MedicalCheckup { get; set; }
        public decimal percentage { get; set; }
        public int totalRecords { get; set; }
        public List<PreEnrollCandidateLink> PreEnrollCandLink { get; set; }
        public int active { get; set; }
        public int Average { get; set; }
        public bool MakerChecker { get; set; }
    }
}

   

