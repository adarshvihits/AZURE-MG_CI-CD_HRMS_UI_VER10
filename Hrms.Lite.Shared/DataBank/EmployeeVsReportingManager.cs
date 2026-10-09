using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeVsReportingManager
    {
        
            public int ActiveCount { get; set; }
        public string Mode { get; set; }
        public int code { get; set; }
        public int AuthorityCode { get; set; }
        public string Name { get; set; }
        public Filter Filter { get; set; }
        public int TabIndex { get; set; }
        public List<EmployeeVsReportingManagerList> EmployeeVsReportingManagerList { get; set; }
        public string EmployeeName { get; set; }
        public string AuthorityName { get; set; }
        
        public string Remarks { get; set; }

        public string EmployeeId { get; set; }
        public string AuthorityId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int APcode { get; set; }
        public int EPcode { get; set; }
        public AuthorityLevel RequestLevel { get; set; }
    }
    public class EmployeeVsReportingManagerList
    {
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public int code { get; set; }
        public string AuthorityName { get; set; }
        public string AuthorityId { get; set; }
        public Guid AuthorityGI { get; set; }
        public int AuthorityCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
