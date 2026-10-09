using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrnParticipantSelection
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime DOJ { get; set; }
        public string TrnStatus { get; set; }
        public string Action { get; set; }
        public int ScheduleCode { get; set; }
        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public bool applicable { get; set; }
        public List<TrnParticipantSelection> TrnParticipantList { get; set; }
    }
}
