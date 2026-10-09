using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class StaffConfirmationReport
    {

        public string Remarks { get; set; }
        public Filter Filter { get; set; }
        public Guid EmployeeGI { get; set; }
        public Dropdown Month { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<StaffConfirmationReportList> StaffConfirmationReportList { get; set; }
        public List<StaffConfirmationReportList> StaffConfirmationReportDetails { get; set; }
    }

    public class StaffConfirmationReportList
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int Count { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Region { get; set; }
        public string EmployeeStatus { get; set; }
        public string FunctionalDesignation { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime EmployeeLWD { get; set; }
        public DateTime EmployeeResignationdate { get; set; }
        public DateTime EmployeeAbscondingDate { get; set; }

    }
}
