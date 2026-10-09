using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class StaffDeclarationAuthority
    {
       
            
            public int ActiveCount { get; set; }

        
        public Filter Filter { get; set; }
            public List<StaffDeclarationAuthorityList> StaffDeclarationAuthorityList { get; set; }
        public List<StaffDeclarationUncheckList> StaffDeclarationUncheckList { get; set; }
    }
        public class StaffDeclarationAuthorityList
    {
            public int EmployeeCode { get; set; }
            public string EmployeeId { get; set; }
            public Guid EmployeeGI { get; set; }
            public string Name { get; set; }
            public int active { get; set; }
            public int totalRecords { get; set; }
            public bool Applicable { get; set; }
            
        }
    public class StaffDeclarationUncheckList
    {
        public int EmployeeCode { get; set; }

    }

}
