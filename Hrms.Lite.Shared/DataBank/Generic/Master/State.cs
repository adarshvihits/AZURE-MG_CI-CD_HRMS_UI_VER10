using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class State : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }        
        public Country Country { get; set; }
        //public ResponseEntity ResponseEntity { get; set; }
        public List<State> StateList { get; set; }
        public string CountryName { get; set; }

        public SelectList DropdownData { get; set; }
    }
}
