using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class ServiceRecord
    {
        public List<ServiceRecord_EmployeeList> EmployeeList { get; set; }
        public ServiceRecord_Details Details { get; set; }
        public Url Url { get; set; }
    }
    public class ServiceRecord_EmployeeList
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
    }
    public class ServiceRecord_Details
    {
        public Guid EmployeeGI { get; set; }
        public string NameOfEstabilshment { get; set; }
        public string NameOfEmployee { get; set; }
        public string NameOfFather_Husband { get; set; }
        public string Age { get; set; }

        public string FullResidentialAddress { get; set; }
        public string Sex { get; set; }
        public string DateOfEntryIntoService { get; set; }
        public string Category_Designation { get; set; }
        public string DateOfTermination { get; set; }
        public List<ServiceRecord_GrowthHistory> GrowthHistory { get; set; }
    }
    public class ServiceRecord_GrowthHistory
    {
        public string EffectiveDate { get; set; }
        public string Location { get; set; }
        public int BasicPay { get; set; }
        public int OtherEmoulments { get; set; }
        public int Total { get; set; }
    }
}
