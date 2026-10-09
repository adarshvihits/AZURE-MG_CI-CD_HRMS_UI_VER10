using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class Grievance
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Filter Filter { get; set; }
        public int GievanceCode { get; set; }
        public int SlNo { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid GrievanceGI { get; set; }
        public string RefNo { get; set; }
        public DateTime RequestedDate { get; set; }
        public GrievanceAuthority Category { get; set; }
        public string Gievance { get; set; }
        public string Suggestion { get; set; }
        public string ActionTaken { get; set; }
        public string Status { get; set; }
        public EmployeePrimaryInfo Employee { get; set; }
        public List<Grievance> GrievanceList { get; set; }
        public List<GrievanceTracker> TrackerList { get; set; }
        public File Attachment { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Designation { get; set; }
        public string AbsoluteUri { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }

        public string AuthName { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? ActionTakenDate { get; set; }

        public File File { get; set; }

    }
    public class GrievanceTracker : Grievance
    {
        public DateTime? AcceptedOn { get; set; }
        public DateTime? ActionTakenOn { get; set; }
    }
}

