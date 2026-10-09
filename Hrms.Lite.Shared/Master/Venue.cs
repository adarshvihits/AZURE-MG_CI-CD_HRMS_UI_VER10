using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Venue:MasterBase
    {
        public string AreaName { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string ResponsiblePerson { get; set; }
        public string DoorNumAndBuilding { get; set; }
        public string Street { get; set; }
        public string StateAndCountry { get; set; }
        public List<Venue> VenueList { get; set; }
        public Region Region { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
      
        public string WebAdderss { get; set; }
      
        public SelectList DropdownData { get; set; }
    }
}
