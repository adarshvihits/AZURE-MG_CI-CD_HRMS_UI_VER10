using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
   public class HRPlanning 
    {
        
        public HRPlanningDropdwn HRPlanningName { get; set; }
        public HRPlanningDropdwn HRPlanningNameMain { get; set; }
        public int HRPlanningSettingsId { get; set; }
        public Guid HRPlanningSettingsGI { get; set; }
        public string PlanningPeriod { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDateAddNew { get; set; }
        public DateTime ToDateAddNew { get; set; }
        public HRPlanningDropdwn Combination1 { get; set; }
        public HRPlanningDropdwn Combination1Main { get; set; }
        public Region Region { get; set; }
        public State State { get; set; }
        public int BasedOn { get; set; }
        public DateTime?  PreviousMonth { get; set; }
        public HRPlanningDropdwn Month { get; set; }

        public int Combination1Code { get; set; }
        public int Combination2Code { get; set; }

        public string Combination1Name { get; set; }
        public string Combination2Name { get; set; }
        public string Combination1Label { get; set; }
        public string Combination2Label { get; set; }
        public int CurrentManPower { get; set; }
        public int PlannedManPower { get; set; }
        public int ManPowerVariance { get; set; }
        public int CurrentGP { get; set; }
        public int PlannedBudget { get; set; }
        public int  BudgetVariance { get; set; }
        public int ListCount { get; set; }
        public List<HRPlanning> HRPlanningList { get; set; }
        public List<HRPlanningSettingsList> HRPlanningSettingsList { get; set; }
        public int NoticePeriodEmployees { get; set; }
        public List<EmployeeDT> EmployeeDtList { get; set; }
        public Filter Filter { get; set; }
    }
    public class HRPlanningSettingsList
    {
        public int Combination2TrnCode { get; set; }
        public string Combination2TrnName { get; set; }
        public int ManPower { get; set; }
        public int Budget { get; set; }
    }
    public class HRPlanningDropdwn:MasterBase
    {

    }
   
}
