using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class AppraisalDueList
    {

        public Filter Filter { get; set; }

        public int duration { get; set; }

        public int Type { get; set; }

        public int EmployeeCode { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public DateTime? DOJ { get; set; }

        public string PayrollGroup { get; set; }

        public DateTime? RenewalDate { get; set; }

        public int active { get; set; }

        public int totalRecords { get; set; }

        public List<AppraisalDueList> AppraisalDueListList { get; set; }


    }
}
