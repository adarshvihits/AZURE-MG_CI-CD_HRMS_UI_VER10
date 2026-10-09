using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class MonthlyReport
    {
        public int ScheduleCode { get; set; }
        public string Training { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParticipantsSelected { get; set; }
        public int ParticipantsCompleted { get; set; }
        public int Traineecost { get; set; }
        public int Trainercost { get; set; }
        public int Trainingcost { get; set; }
        public int Totalcost { get; set; }
        public int Month { get; set; }
        public string Completionstatus { get; set; }
        public Filter filter { get; set; }
        public List<MonthlyReport> MonthlyReportList { get; set; }
        public List<ParticipantDetails> ParticipantDetails { get; set; }
    }
    public class ParticipantDetails
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
    }
}
