using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class YearlySalary
    {
        
        public FinancialYear FinancialYear { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmpId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<YearlySalary_EmployeeList> EmployeeList { get; set; }
        public List<dynamic> Data { get; set; }
        public YearlySalary_MainList MainList { get; set; }
        public int EmployeeCode { get; set; }
    }
    public class YearlySalary_EmployeeList
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime DOJ { get; set; }
    }
    public class YearlySalary_MainList
    {
        public List<YearlySalary_Header> Header { get; set; }
        public List<YearlySalary_Info> YearlySalary_Info { get; set; }
        public List<YearlySalary_Details> YearlySalary_Details { get; set; }


    }
    public class YearlySalary_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class YearlySalary_Info
    {
        public int ID { get; set; }
        public string ROWTYPE { get; set; }
        public string TITLE { get; set; }
        public decimal Total { get; set; }

    }
    public class YearlySalary_Details
    {
        public decimal Amount { get; set; }

    }
}

