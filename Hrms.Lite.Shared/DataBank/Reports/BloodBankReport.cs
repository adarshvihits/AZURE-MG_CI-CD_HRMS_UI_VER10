using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class BloodBankReport
    {
       
        public List<BloodBankReportList> BloodBankReportList { get; set; }
        public Filter filter { get; set; }
        public int[] BloodGroup { get; set; }

        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
        //public BloodGroup BloodGroups { get; set; }
    }
    public class BloodBankReportList
    {
        public Guid EmployeeGI { get; set; }
        public string AbsoluteUri { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string BloodGroup { get; set; }
        public string PhoneNumber { get; set; }
        public string MailId { get; set; }
        public string Address { get; set; }
        public string OfficeEmail { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
    }
  
}
