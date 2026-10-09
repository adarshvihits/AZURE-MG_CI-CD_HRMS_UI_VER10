using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
public class DailyFandFSalaryProcessing
    {
        public Dropdown PayrollMonth { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public bool Applicability { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public Filter Filter { get; set; }
        public List<DailyFandFSalaryProcessing> DailyFandFSalaryProcessingList { get; set; }
        public List<DailyFandFProcessing_popup> DailyFandFProcessing_popup { get; set; }
    }
public class DailyFandFProcessing_popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NET_SALARY { get; set; }
        public string Remarks { get; set; }

    }
}
