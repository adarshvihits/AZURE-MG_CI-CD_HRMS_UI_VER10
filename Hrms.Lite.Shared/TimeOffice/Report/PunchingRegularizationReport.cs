using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
   public  class PunchingRegularizationReport
    {
        public string Date { get; set; }
        public Month Month { get; set; }
        public DateTime? INTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string Remarks { get; set; }
        public int Reason { get; set; }
        public Filter Filter { get; set; }
        public string TabIndex { get; set; }
        public List<PunchingRegularizationList> PunchingRegularizationList { get; set; }


        public List<Tracker> Tracker { get; set; }
        public Guid PunchRegularizationGI { get; set; }
        public decimal TotalDays { get; set; }
        public int ApproveCount { get; set; }
        public int RejectedCount { get; set; }
        public int PendingCount { get; set; }
        public int TotalCount { get; set; }
     

        public MasterBase LeavePeriod { get; set; }

    }

    public class PunchingRegularizationList

    {
        public DateTime AppliedOn { get; set; }
        public DateTime Date { get; set; }
        public string AssignedShift { get; set; }
        public string IN { get; set; }
        public string Out { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string Status { get; set; }
        public string AbsentReason { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public Guid PunchRegularizationGI { get; set; }
        public int ApproveCount { get; set; }
        public int RejectedCount { get; set; }
        public int PendingCount { get; set; }
        public int TotalCount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
