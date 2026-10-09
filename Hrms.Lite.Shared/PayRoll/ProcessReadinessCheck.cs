using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;


namespace Hrms.Lite.Shared.PayRoll
{
   public class ProcessReadinessCheck
    {
       public Dropdown PayrollGroup { get; set; }
        public Filter Filter { get; set; }
        public Dropdown Month { get; set; }
        public int listtype { get; set; }
        public List<ProcessReadinessCheckList> ProcessReadinessCheckList { get; set; }
        public List<ProcessReadinessCheck_Popup> ProcessReadinessCheck_Popup { get; set; }//Salary list Popup
        public MonthlyAttendanceProcessing MonthlyAttendanceProcessing { get; set; }

    }
    public class ProcessReadinessCheckList
    {
        public int SlNo { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
        public string ProcessType { get; set; }
        public int AttType { get; set; }
        public string FormType { get; set; }
        public string Impact { get; set; }
    }

    public class ProcessReadinessCheck_Popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public int LOP { get; set; }
        public DateTime ExDOJ { get; set; }
        public string Bank { get; set; }
        public string AccNo { get; set; }
        public string IFSC { get; set; }
        public string PrevAccNo { get; set; }
        public string CurrAccNo { get; set; }
        public string PrevPayrollGroup { get; set; }
        public string CurrPayrollGroup { get; set; }
        public string PFApplicable { get; set; }
        public string OldPFGroup { get; set; }
        public string NewPFGroup { get; set; }
        public string PrevPFApplicability { get; set; }
        public string CurrPFApplicability { get; set; }
        public string GrossSalary { get; set; }
        public string OldEsiGroup { get; set; }
        public string NewEsiGroup { get; set; }
        public string PrevEsiApplicability { get; set; }
        public string CurrEsiApplicability { get; set; }
        public string Stage { get; set; }
        public string JobTransitionType { get; set; }
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NET_SALARY { get; set; }
        public string Remarks { get; set; }
    }
}
