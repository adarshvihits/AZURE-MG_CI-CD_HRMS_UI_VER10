using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class Location : MasterBase
    {
      
        public string AreaName { get; set; }
       
        public Dropdown AreaDropdown { get; set; }
        public Dropdown FunctionalState { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string DoorNumAndBuilding { get; set; }
        public string Street { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public District District { get; set; }
        public List<Location> LocationList { get; set; }
        public LocationType LocationType { get; set; }
        public Region Region { get; set; }
        public string LandLineNum { get; set; }
        public string EmailId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public PTGroup PTgroup { get; set; }
        public ESIGroup ESIgroup { get; set; }
        public string WebAdderss { get; set; }
        public string GeoAdderss { get; set; }
        public string StatutoryRegistration { get; set; }
        public string TaxEmployee { get; set; }
        public string TaxEmployer { get; set; }
        public string WelfareFund { get; set; }
        public string LicenseNumber { get; set; }
        public string LINNumber { get; set; }
        public string SLN { get; set; }
        public string? ERPCode { get; set; }
        public DateTime? OpeningDate { get; set; }
        public bool UpcomingStatus { get; set; }
        public bool PayrollStatus { get; set; }
        public Dropdown Category { get; set; }
      
        public Dropdown BusinessType { get; set; }
        public Dropdown Zone { get; set; }
        public Dropdown Cluster { get; set; }
        public Dropdown Metro { get; set; }
        public int Emirates { get; set; }
        public decimal? OverTimeHr { get; set; }
        public decimal? LWFAmount { get; set; }              
        public decimal? LWFAmountEmployer { get; set; }     
        public string M365CostCenter { get; set; }
        public string WelfareAccNo { get; set; }
        public string EstRegNo { get; set; }



        public SelectList DropdownData { get; set; }
    }
    public class LocationType : MasterBase
    {
    }

   
}
