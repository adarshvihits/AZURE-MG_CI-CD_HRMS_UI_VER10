using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeVsGeoTagging
    {        
        public Filter filter { get; set; }
        public List<EmployeeVsGeoTaggingList> EmployeeVsGeoTaggingList { get; set; }
        public EmployeeVsGeoTaggingList EmployeeVsGeoTaggingDetails { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int?[] ApplicablePunchingLocations { get; set; }
    }
    public class EmployeeVsGeoTaggingList
    {
        public int EmployeeCode { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string DESIGNATION { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string PunchingLocations { get; set; }
        public string OutsidePunching { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public bool OutsidePunchingApplicable { get; set; }
    }
}
