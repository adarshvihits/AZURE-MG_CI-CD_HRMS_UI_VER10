using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class RewardsAndRecognition
    {
        public EmployeeHeader EmployeeHeader { get; set; }
         public  int EmpCode { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int PointSummaryCount { get; set; }
        public int MoreTabCount { get; set; }
        public int PendingTabCount { get; set; }
        public  RewardsRequest  RewardsRequest { get; set; }
        public List<RewardsRequest>  PointSummaryDT{ get; set; }
    public  RewardsAndRecognitionFilterCombo RewardsAndRecognitionFilterCombo {get;set;}
        public List<RewardsAndRecognitionList >RewardsAndRecognitionList  { get; set; }
        public List<RewardandRecognitionTabs> RewardandRecognitionTabs { get; set; }

        public Filter Filter { get; set; }
        public int TabCode { get; set; }
        public int RewardsCode { get; set; }
    }
    public class RewardsAndRecognitionList
    {
        public int RewardsAndRecognitionCode { get; set; }
        public Guid RewardsAndRecognitionGI { get; set; }
         public string EmpId { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
         public RewardsRequest RewardsRequest { get; set; }
      }
    public class RewardandRecognitionTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    
  public class RewardsRequest
    {
        public Guid RewardsAndRecognitionGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public  Guid EmployeeGI { get; set; }
        public Rewards Rewards { get; set; }
        public DateTime? RewardDate { get; set; }
        public int RewardRecommendedBy { get; set; }
        public string RecommendedEmpName { get; set; }
        
       public string RecommendedFor  { get; set; }
       public string  EntryUserDetails { get; set; }
        public  PointCriteria  PointCriteria { get; set; }
        public DateTime EntryDate { get; set; }
        public string Remarks { get; set; }
       public int  TotalPoints { get; set; }
        public File  File { get; set; }
    public List<RewardsLog> RewardsLog { get; set; }

    }
    public class RewardsLog
    {
        public RewardsRequest RewardsRequest { get; set; }

    }

}