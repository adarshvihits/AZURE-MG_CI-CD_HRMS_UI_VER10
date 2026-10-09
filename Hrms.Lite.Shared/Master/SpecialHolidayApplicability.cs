using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class SpecialHolidayApplicability 
    {
        public CalendarYear Year { get; set; }
        public int EmpCode { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime Doj { get; set; }
        public int HolidayCode { get; set; }
        public Dropdown Holiday { get; set; }
        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public string Mode { get; set; }
        public bool Applicability { get;set; }
        public UserBase User { get; set; }

        public List<SpecialHolidayApplicability> SpecialHolidayEmpList { get; set; }

    }
}
