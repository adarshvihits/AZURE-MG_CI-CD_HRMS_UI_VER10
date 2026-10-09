using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class PTProcessing
    {
        public UserBase User { get; set; }
        public Filter filter { get; set; }
       
        public PayrollMonth Month { get; set; }
        public PTGroup PTGroup { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int SalaryMonths { get; set; }
       public  PTProcessing_Details PTProcessing_Details { get; set; }
        public string Duration { get; set; }
        public List<PTProcessing_Details> Details { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
    }
    public class PTProcessing_Details
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ActualPTApplicabileSalary { get; set; }
        public int ProcessedMonth { get; set; }
        public int ProcessedSalary { get; set; }
        public int NotProcessedMonth { get; set; }
        public int NotProcessedSalary { get; set; }
        public int PTSalary { get; set; }
        public int Basic { get; set; }
        public int OtherAllowance { get; set; }
        public int PTAmount { get; set; }
        public int PTAlreadyPaid { get; set; }
        public int CurrentMonthPT { get; set; }
        public int RemainingAmount { get; set; }
        public int DividingPeriod { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
