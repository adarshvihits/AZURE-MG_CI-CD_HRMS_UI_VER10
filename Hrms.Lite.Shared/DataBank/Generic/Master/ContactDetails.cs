using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class ContactDetails
    {
        public string House { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public District District { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string MobileNo { get; set; }
        public string MailID { get; set; }
    }
}
