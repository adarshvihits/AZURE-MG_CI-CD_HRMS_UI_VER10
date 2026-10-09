using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class AppointmentLetterSubmissionReport
    {
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DOJ { get; set; }
        public string Department { get; set; }
        public string FunDesignation { get; set; }
        public string Location { get; set; }
        public DateTime AcceptedDate { get; set; }
        public string PDF { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int TrnCode { get; set; }


        public List<AppointmentLetterSubmissionReport> AppointmentLetterSubmissionReportList { get; set; }



    }
}
