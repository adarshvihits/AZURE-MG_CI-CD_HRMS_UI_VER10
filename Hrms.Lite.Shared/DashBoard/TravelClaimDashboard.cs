using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
   public class TravelClaimDashboard
    {
        public int TotalRqst { get; set; }
        public int ApprovalPendingRqst { get; set; }
        public int ApprovedRqst { get; set; }
        public int RejectedRqst { get; set; }
        public Dropdown Type { get; set; }
        public List<RequestList> requestLists { get; set; }
        public List<BudjetTracking> BudjetTrackings { get; set; }
    }
    public class RequestList
    {
        public int DeptCode { get; set; }
        public string DeptName { get; set; }
        public int Deptcount { get; set; }
    }
    public class BudjetTracking
    {
        public int DeptCode { get; set; }
        public string DeptName { get; set; }
        public int ApprovedAmount { get; set; }
        public int AdvanceAmount { get; set; }
    }
}
