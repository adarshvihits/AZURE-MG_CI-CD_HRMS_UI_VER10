using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
   public class EmployeeAddress
    {
        public ContactDetails PermanentAddress { get; set; }
        public ContactDetails PresentAddress { get; set; }
        public bool SameAsAddressCheck { get; set; }
    }
}
