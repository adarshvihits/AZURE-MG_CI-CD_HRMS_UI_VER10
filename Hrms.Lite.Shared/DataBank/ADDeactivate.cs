using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class ADDeactivate
    {
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public string Mode { get; set; }
        public int code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Filter Filter { get; set; }
        public int TabIndex { get; set; }
        public List<ADDeactivateList> ADDeactivateList { get; set; }
       
        public DateTime? DeactivateDate { get; set; }
        public string Remarks { get; set; }

        public string EmployeeId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
    }
    public class ADDeactivateList
    {
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public int code { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
