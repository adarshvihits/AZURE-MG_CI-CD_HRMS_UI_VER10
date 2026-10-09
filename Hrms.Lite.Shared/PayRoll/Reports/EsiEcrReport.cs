using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class EsiEcrReport
    {
   
        public int Month { get; set; }
        public int ESIGroup { get; set; }
        public List<EsiEcrSummary> EsiEcrSummary { get; set; }
        public Filter Filter { get; set; }
        public object Details { get; set; }
    }
    public class EsiEcrSummary

    {
        public int SlNo { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        // public string EmployeeID { get; set; }
        public string IpName { get; set; }
        public string IpNumber { get; set; }
        public decimal NoDaysWagepaid { get; set; }
        public decimal ToatalMonthWage { get; set; }
        public string ReasonCode { get; set; }
        public string LastWrkingDay { get; set; }
        public string ESIGroupCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
