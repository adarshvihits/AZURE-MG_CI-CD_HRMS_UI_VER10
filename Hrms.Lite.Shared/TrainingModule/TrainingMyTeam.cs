using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingMyTeam : MasterBase
    {

        public int ScheduleCode { get; set; }
        public int TrainingGroupCode { get; set; }
        public int EmployeeCode { get; set; }
        public Guid? EmployeeGI { get; set; }
        public string ScheduleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; }
        public string Trainer { get; set; }
        public string Venue { get; set; }
        public int Availability { get; set; }
        public string Mandatory { get; set; }
        public int MaximumHeadCount { get; set; }
        public string CompletionStatus { get; set; }
        public List<TrainingMyTeam> TrainingMyTeamList { get; set; }
        public List<TrainingApplEmployees> EmployeeList { get; set; }
    }


    public class TrainingApplEmployees
    {
        public int ScheduleCode { get; set; }
        public int TrainingGroupCode { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public bool active { get; set; }




    }
}
