using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
  public  class p2pRecognition
    {
        public int RewardCode { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int radiomode { get; set; }
        public int RRCode { get; set; }
        public Rewards Rewards { get; set; }
        public PointCriteria PointCriteria { get; set; }
        public string Recommendedby { get; set; }
        public string RecommendedbyEmpid { get; set; }
        public string Recommendedfor { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Remarks { get; set; }
        public string EntryDate { get; set; }
        public string RewardDate { get; set; }
        public string ReplyStatus { get; set; }
        public string Reply { get; set; }
        public Filter SliderFilter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<p2pRecognition> p2pRecognitionList { get; set; }
    }
}
