using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class InstantMessaging
    {
        
            public string Mode { get; set; }
            public int MessageCode { get; set; }
            public String Title { get; set; }
            public String Message { get; set; }
            public int EmployeeCount { get; set; }
            public List<InstantMessaging> MessagesList { get; set; }
            public List<ApplicableEmployees> ApplicableEmployeesList { get; set; }
            public Filter filter { get; set; }
    }
    public class ApplicableEmployees
    {
            public int EmployeeCode { get; set; }
            public String EmployeeID { get; set; }
            public String EmpName { get; set; }
            public String Location { get; set; }
            public String Division { get; set; }
            public String Grade { get; set; }
            public String Department { get; set; }
            public String Designation { get; set; }
            public bool Applicable { get; set; }
    }
}
