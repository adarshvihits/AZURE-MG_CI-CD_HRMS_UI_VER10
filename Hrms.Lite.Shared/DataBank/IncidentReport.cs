using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
  public  class IncidentReport
    {
        public int IncidentCode { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int radiomode { get; set; }
        public bool radiostatus { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Incidentname { get; set; }
        public string EntryDate { get; set; }
        public string OccuredDate { get; set; }
        public string ReplyStatus { get; set; }
        public string Reply { get; set; }
        public File Attachment { get; set; }
        public Filter SliderFilter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<IncidentReport> IncidentReportList { get; set; }
    }
}
