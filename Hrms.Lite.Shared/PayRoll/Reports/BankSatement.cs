using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class BankStatement
    {
     public int count { get; set; }


        public Bank Bank { get; set; }
        public List<BankStatement_Details> Details { get; set; }
        public List<AccountStatement_Details> AccountStatement { get; set; }
        public Month Month { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public FilterMultiSelect Filter { get; set; }
        public int[] Banks { get; set; }
        public MasterBase BankStatementFormat { get; set; }
        public List<AccountStatement_EmployeeList> EmployeeList { get; set; }
        public int Code { get; set; }
        public string TYPE { get; set; }
        public string MonthName { get; set; }
        public string Description { get; set; }
        public int Total_NetAmount { get; set; }
        public Dropdown Allowance { get; set; }
    }
    public class BankStatement_Details
    {
        public int totalRecords { get; set; }
        public int active { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Bank { get; set; }

        public int?[] Banks { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public int NetAmount { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
    }
    public class AccountStatement_Details
    {

        public int ID { get; set; }
        public int Code { get; set; }
        public string TYPE { get; set; }
        public string Description { get; set; }
        public int EmployeeCount { get; set; }
        public int Amount { get; set; }
        public bool Link { get; set; }
    }

    public class AccountStatement_EmployeeList
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public decimal Amount { get; set; }
    }
}
