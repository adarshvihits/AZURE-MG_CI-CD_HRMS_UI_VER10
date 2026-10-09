using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class EmployeeHeader
    {
        public Guid EmployeeGuid { get; set; }
        public string  EmployeeName {get;set;}
        public string ReportingManager { get; set; }
        public string  EmployeeAbsoluteUri { get; set; }
        public string EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public PositionManagement Position { get; set; }
        public string LWD { get; set; }
        public DateTime DOJ { get; set; }
        public bool ActiveStatus { get; set; }
        public string FileName { get; set; }
        public int TypeCode { get; set; }
        public string FunctionalDesignation { get; set; }
        public string LocationCode { get; set; }
    }
}
