using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class GrievanceAuthority : MasterBase
    {
    
        public int SlNo { get; set; }
        public string RefNo { get; set; }
        public DateTime RequestedDate { get; set; }
        public string CategoryName { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int CategoryCode { get; set; }
        public EmployeePrimaryInfo EmployeeInfo { get; set; }
        public List<EmployeePrimaryInfo> EmpInfo { get; set; }
        public List<GrievanceAuthority> GrievanceAuthorityList { get; set; }
    }
}
