using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ParentsMediclaimRequestReport
    {
        public int ActiveCount { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Filter Filter { get; set; }

        public List<ParentsMediclaimRequestReportList> ParentsMediclaimRequestReportList { get; set; }

        public string Remarks { get; set; }

        public string EmployeeId { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int FinancialYearcode { get; set; }
    }
    public class ParentsMediclaimRequestReportList
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public int code { get; set; }

        public int totalRecords { get; set; }
        public int active { get; set; }
        public string ParentName { get; set; }
        public string Relation { get; set; }
        public string Gender { get; set; }
        public DateTime? ParentDOB { get; set; }
        public DateTime? RequestDate { get; set; }
        public decimal SumInsured { get; set; }
        public decimal EmiAmount { get; set; }
        public int EMIPeriod { get; set; }

    }
}
