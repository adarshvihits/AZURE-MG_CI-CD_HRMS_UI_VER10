using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    //public class LeaveLaps
    //{

    //    public int EmployeeCode { get; set; }
    //    public string EmployeeID { get; set; }
    //    public string EmployeeName { get; set; }
    //    public Guid EmployeeGI { get; set; }
    //    public string Designation { get; set; }
    //    public string Department { get; set; }
    //    public string Location { get; set; }
    //    public decimal Balance { get; set; }   
    //    public decimal Lop { get; set; }
    //    public decimal MonthlyTaken { get; set; }
    //    public List<LeaveLaps> LeaveLapsDetails { get; set; }
    //    public decimal MandatoryTaken { get; set; }
    //    public Filter Filter { get; set; }   
    //    public decimal LapsCount { get; set; }
    //    public int PayrollMonth { get; set; }
    //    public int PayrollGroup { get; set; }
    //    public int Leave { get; set; }
    //    public int LeaveGroup { get; set; }
    //    public int totalRecords { get; set; }
    //    public int active { get; set; }

    //}


    public class LeaveLaps
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Location { get; set; }
        public decimal Balance { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Taken { get; set; }
        public decimal MandatoryTaken { get; set; }
        public decimal LapsCount { get; set; }
        public List<LeaveLaps> LeaveLapsList { get; set; }  
        public Filter Filter { get; set; }      
        public int LeaveCode { get; set; }
        public int LeavePeriodCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }

}
