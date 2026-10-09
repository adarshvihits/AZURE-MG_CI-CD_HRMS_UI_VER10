using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PfEcrReport
    {
        public int Month { get; set; }
        public int PFGroup { get; set; }
        public int Format { get; set; }
        public Filter Filter { get; set; }
        public List<PfEcrSummary> PfEcrSummary { get; set; }
    }
    public class PfEcrSummary
    {
        public int SlNo { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string MemberName { get; set; }
        public string UAN { get; set; }
        public int GrossWage { get; set; }
        public int EpfWage { get; set; }
        public int EpsWage { get; set; }
        public int EdliWage { get; set; }
        public int EpfContriRemitted { get; set; }
        public int EpsContriRemitted { get; set; }
        public int EpfEpsDiffRemitted { get; set; }
        public int Ncpdays { get; set; }
        public int RefundAdvance { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
    }
}

