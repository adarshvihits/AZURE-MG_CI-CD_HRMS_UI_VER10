using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingDueList
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? DOJ { get; set; }
        public string TrainingGroup { get; set; }
        public string Duration { get; set; }
        public DateTime? Lastattended { get; set; }
        public DateTime? Duedate { get; set; }

        public int trainingcode { get; set; }

        public int Ageing { get; set; }
        public string Training { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int totalcount { get; set; }

        public List<TrainingDueList> TrainingDueRptList { get; set; }
       
    }
}
