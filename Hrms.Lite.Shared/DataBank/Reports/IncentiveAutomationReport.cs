using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class IncentiveAutomationReport
    {
        public Filter Filter { get; set; }
        public Dropdown Month { get; set; }
        public String EmployeeID { get; set; }
        public String EmployeeName { get; set; }
        public String AppraisalType { get; set; }
        public String DateOfChange { get; set; }
        public String OldLocation { get; set; }
        public String NewLocation { get; set; }
        public String InceLocation { get; set; }
        public String OldDesignation { get; set; }
        public String NewDesignation { get; set; }
        public String InceDesignation { get; set; }
        public String OldFunDesignation { get; set; }
        public String NewFunDesignation { get; set; }
        public String InceFunDesignation { get; set; }
        public String OldCostCenter { get; set; }
        public String NewCostCenter { get; set; }
        public String InceCostCenter { get; set; }

        public int Active { get; set; }
        public int totalRecords { get; set; }
        
        public List<IncentiveAutomationReport> IncentiveAutomationReportList { get; set; }
    }
}
