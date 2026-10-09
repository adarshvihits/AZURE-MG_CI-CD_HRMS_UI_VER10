using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class LeaveEncashmentProcessing
    {


        public List<LeaveEncashmentDetails> LeaveEncashmentList { get; set; }
        public List<LeaveEncashmentDetails> LeaveEncashmentProcess { get; set; }
        public List<LeaveEncashmentDetails> EncashmetProcessingReport { get; set; }
        public int Month { get; set; }
        public int Allowance { get; set; }
        public Filter filter { get; set; }

    }


    public class LeaveEncashmentDetails
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int MonthCode { get; set; }
        public int AllowanceCode { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int NoOfLeave { get; set; }
        public decimal Amount { get; set; }
        public int totalRecords { get; set; }
        public int Active { get; set; }

    }
}
