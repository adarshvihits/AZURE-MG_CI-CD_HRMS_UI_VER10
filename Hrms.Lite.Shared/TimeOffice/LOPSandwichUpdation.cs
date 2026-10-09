using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class LOPSandwichUpdation
    {
        public Month Month { get; set; }
        public int Payroll { get; set; }

        public Filter Filter { get; set; }

        public List<LOPSandwichUpdationList> LOPSandwichUpdationList { get; set; }


        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PrevDaystatus { get; set; }
        public string NextdatStatus { get; set; }
        public bool Applicable { get; set; }
        public int PrevDayStatusCode { get; set; }
        public int NextDayStatusCode { get; set; }
    }

    public class LOPSandwichUpdationList
    {
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PrevDaystatus { get; set; }
        public string NextdatStatus { get; set; }
        public bool Applicable { get; set; }
        public int PrevDayStatusCode { get; set; }
        public int NextDayStatusCode { get; set; }

    }
}
