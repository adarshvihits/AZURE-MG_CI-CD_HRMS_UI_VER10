using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class DisciplinaryIncidents
    {
        public Guid DisciplinaryIncidentGI { get; set; }
        public int DisciplinaryIncidentId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public FilterSlabComboFill DurationwiseFilterSlab { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public IncidentsAction IncidentsAction { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public List<DisciplinaryIncidentsList> DisciplinaryIncidentsList { get; set; }
        public List<DisciplinaryIncidentsTabs> DisciplinaryIncidentsTabs { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int MoreTabCount { get; set; }
        public int PendingTabCount { get; set; }
        public Filter Filter { get; set; }
        public int TabCode { get; set; }
        public int IncidentCode { get; set; }
    }
    public class DisciplinaryIncidentsList
        {
            public Guid DisciplinaryIncidentGI { get; set; }
            public string EmpId { get; set; }
            public string EmployeeName { get; set; }
            public int EmployeeCode { get; set; }
            public Guid EmployeeGI { get; set; }
            public Designation Designation { get; set; }
            public Department Department { get; set; }
            public Location Location { get; set; }
            public string Incident { get; set; }
            public string IncidentType { get; set; }
            public DateTime Incidentdate { get; set; }
            public string ActionTaken { get; set; }
        public  string Narration { get; set; }


        }

        public class Incident:MasterBase
        {

        }
        public class IncidentType : MasterBase
        {

        }
        public class ActionTaken : MasterBase
        {

        }
    public class DisciplinaryIncidentsTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class Incidents
         {
        public string DisciplinaryCode { get; set; }
        public string Incident { get; set; }
        public string IncidentType { get; set; }
        public DateTime? Incidentdate { get; set; }
        public string ActionTaken { get; set; }
        public string ReportedBy { get; set; }
        public DateTime? Reporteddate { get; set; }
        public string Narration { get; set; }
        public string FileUri{ get; set; }
        public File File { get; set; }

    }
    public class IncidentsAction
        {
            public Guid DisciplinaryIncidentGI { get; set; }
            public EmployeeHeader EmployeeHeader { get; set; }
            public Guid EmployeeGI { get; set; }
            public Incidents Incidents { get; set; }
            public Incident Incident { get; set; }
            public IncidentType IncidentType { get; set; }
            public DateTime? Incidentdate { get; set; }
            public DateTime? Reportedtdate { get; set; }
            public string Narration { get; set; }
            public ActionTaken ActionTaken { get; set; }
            public DateTime EntryDate { get; set; }
            public string ReportedBy { get; set; }
            public File File { get; set; }
            public List<IncidentLog> IncidentLog { get; set; }
        }
        public class IncidentLog
        {
        public Guid DisciplinaryIncidentGI { get; set; }
        public Incidents Incidents { get; set; }
    }
}
