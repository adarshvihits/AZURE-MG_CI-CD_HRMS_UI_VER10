using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class FormIVReport
    {
        public Filter Filter { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DOJ { get; set; }
        public string Location { get; set; }
        public string WithEffectFrom { get; set; }
        public int TotalDays { get; set; }
        public decimal Lop { get; set; }
        public decimal PresentDays { get; set; }
        public decimal Amount { get; set; }
        public decimal PF { get; set; }
        public decimal ESI { get; set; }
        public string Download { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }   
        public int monthcode { get; set; }
        public List<FormIVReport> FormIVReportList { get; set; }
        public List<FormIVReportDetails> FormIVReportDetails { get; set; }

    }

    public class FormIVReportDetails
    {
        public int SalaryPeriodCode { get; set; }
        public string SalaryPeriod { get; set; }
        public string Allowance { get; set; }
        public decimal NewAmt { get; set; }
        public decimal OldAmt { get; set; }
        public decimal prevArrear { get; set; }
        public int tDays { get; set; }
        public decimal AmtPerDay { get; set; }
        public int LOP { get; set; }
        public decimal Arrear { get; set; }
        public int preEmp { get; set; }
        public string empDetail { get; set; }
        public decimal totMpArrear { get; set; }

        public int PayDays { get; set; }


    }

}
