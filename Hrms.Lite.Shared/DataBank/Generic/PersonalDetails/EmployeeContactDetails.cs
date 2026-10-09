using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
   public class EmployeeContactDetails
    {
        public string PersonalMobile { get; set; }
        public string PersonalEmail { get; set; }
        public string OfficeMobile { get; set; }
        public string OfficeEmail { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string EmergencyContactNumber { get; set; }
        public Relation Relation { get; set; }

    }
}
