using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class UHID
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public DateTime DOJ { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public List<UHID> UHIDList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public Filter filter { get; set; }
        public string UHIDNumber { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public List<UHIDFamilyDetails> FamilyDetails { get; set; }
    }
    public class UHIDFamilyDetails
    {
        public Guid FamilyLogGI { get; set; }
        public string Name { get; set; }
        public string RelationName { get; set; }
        public FamilyRelation Relation { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public LivingStatus Status { get; set; }
        public string StatusName { get; set; }
        public string UHIDNumber { get; set; }
        public int EmployeeRowId { get; set; }
        public int RelationCode { get; set; }
    }
}
