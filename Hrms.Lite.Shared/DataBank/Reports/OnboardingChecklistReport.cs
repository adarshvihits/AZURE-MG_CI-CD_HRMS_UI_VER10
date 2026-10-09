using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class OnboardingChecklistReport
    {
        public UserBase User { get; set; }
        public List<OnboardingChecklistReportList> OnboardingChecklistReportList { get; set; }
        public Filter Filter { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
       
        public int EmployeeCode { get; set; }
        public string EmployeeNameID { get; set; }
        public string UserEmpNameID { get; set; }
        public string VerificationDate { get; set; }
        public List<OnboardingChecklistDetails> OnboardingChecklistDetails { get; set; }
    }
    public class OnboardingChecklistReportList
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string DOJ { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }

    public class OnboardingChecklistDetails
    {

        public int SlNo { get; set; }
        public string Item { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

    }
}
