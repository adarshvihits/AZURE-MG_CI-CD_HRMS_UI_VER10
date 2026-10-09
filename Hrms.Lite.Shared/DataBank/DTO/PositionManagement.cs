using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.Master;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PositionManagement:MasterBase
    {
        public Filter Filter { get; set; }
        public string type { get; set; }
        public int TotalPosition { get; set; }
        public int ActivePosition { get; set; }
        public int VacantPosition { get; set; }
        public int TemporaryPosition { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public string ApproveStatus { get; set; }
        public string NoofPosition { get; set; }
        public string Remark { get; set; }
        public List<PositionManagement> PositionDetailedList { get; set; }

    }
   
}
