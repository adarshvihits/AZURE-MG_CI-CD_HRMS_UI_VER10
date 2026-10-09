using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class DepartmentvsDeptHead
    {
        public Filter filter;

        public int SlNo { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentCode { get; set; }
        public Dropdown EmployeeInfo { get; set; }

        public string Authority { get; set; }

        public List<DepartmentvsDeptHead> DepartmentvsDeptHeadList { get; set; }


    }
}
