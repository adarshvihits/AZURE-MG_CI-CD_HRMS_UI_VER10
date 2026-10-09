using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class EmployeeVsMinPresentDays
    {
        public Filter Filter { get; set; }
        public int Status { get; set; }
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public List<EmployeeVsMinPresentDaysList> EmployeeVsMinPresentDaysList { get; set; }
    }
    public class EmployeeVsMinPresentDaysList
    {
        public Guid EmployeeGuid { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public DateTime DOJ { get; set; }
        public int? PresentDays { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int Action { get; set; }
    }
}
