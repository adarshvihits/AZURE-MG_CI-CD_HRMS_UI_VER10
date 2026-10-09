using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class PoliceVerification
    {
        public int DoneCount { get; set; }
        public int PendingCount { get; set; }
        public Filter Filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
      public string TabIndex { get; set; }
        public File Attachment { get; set; }
        public int PoliceVerificationCode { get; set; }
        public Guid PoliceVerificationGI { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string PoliceStationDetails { get; set; }
        public string DetailsAuthority { get; set; }
        public string Remarks { get; set; }
        public District District { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public List<PoliceVerificationList> PoliceVerificationList { get; set; }
    }
    public class PoliceVerificationList
    {

        public Guid PoliceVerificationGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string PoliceStationDetails { get; set; }
        public string VerificationStatus { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
