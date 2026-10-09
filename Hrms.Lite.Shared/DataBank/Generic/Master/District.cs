using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class District : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public List<District> DistrictList { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
