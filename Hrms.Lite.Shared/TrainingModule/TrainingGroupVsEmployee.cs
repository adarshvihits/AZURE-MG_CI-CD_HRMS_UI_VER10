using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingGroupVsEmployee
    {
        public List<TrainingGroupVsEmployeeDetails> EmployeeList { get; set; }
        public TrainingGroupVsEmployeeDetails TrainingGroupVsEmployeeDetails { get; set; }
        public Filter Filter { get; set; }
    }
    
    public class TrainingGroupVsEmployeeDetails
    {

        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string YearOfService { get; set; }
        public UserBase User { get; set; }
        public string TrainingGroup { get; set; }
        public int TrainingGroupCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Action { get; set; }
    }
}
