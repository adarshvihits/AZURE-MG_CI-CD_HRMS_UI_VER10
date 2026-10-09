using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class EmpVsMobileRegistration
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string DeviceID { get; set; }

        public Filter filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int totalcount { get; set; }
        public int Status { get; set; }

        public List<EmpVsMobileRegistration> EmpVsMobileRegistrationList { get; set; }

    }
}
