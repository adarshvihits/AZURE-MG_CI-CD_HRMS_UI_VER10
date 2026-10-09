using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class Training_EmployeVsReport
    {
        public int FYCode { get; set; }
        public int TrainingCode { get; set; }
        public int CategoryCode { get; set; }
        public string FYName { get; set; }
        public string Category { get; set; }
        public string Training { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Trainer { get; set; }
        public string TrainingLocation { get; set; }
        public int Cost { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string Hours { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Grade { get; set; }
        public string Designation { get; set; }
        public DateTime DOJ { get; set; }
        //public int TrainingCountSelected { get; set; }
       public int TrainingCountAttended { get; set; }
        public int TotalCost { get; set; }
        public int Active { get; set; }
        public int TotalRecords { get; set; }
        public Filter Filter { get; set; }
        public List<Training_EmployeVsReport> TrainingEmployeeReportList { get; set; }
        public List<popupDT> popupDTList { get; set; }
    }
    public class popupDT
    {
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public string Trainingname { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EmployeeID { get; set; }
        public string Category { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
    }
}


