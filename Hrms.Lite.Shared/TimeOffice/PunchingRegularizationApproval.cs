using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class PunchingRegularizationApproval
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AppliedOn { get; set; }
        public DateTime Date { get; set; }
        public string INTime { get; set; }
        public string OutTime { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public Guid PunchRegularizationGI { get; set; }


        public List<PunchingRegularizationApproval> PunchingRegularizationApprovalList { get; set; }
    }
}
