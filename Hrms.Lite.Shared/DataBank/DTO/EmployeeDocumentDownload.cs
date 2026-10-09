using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeDocumentDownload
    {
        public List<EmployeeDocDownload_List> EmployeeProfileList { get; set; }
    }

    public class EmployeeDocDownload_List
    {
        public int employeeCode { get; set; }
        public string employeeID { get; set; }
        public string fullName { get; set; }
        public string absoluteUrl { get; set; }
        public string FileName { get; set; }

    }
    }
