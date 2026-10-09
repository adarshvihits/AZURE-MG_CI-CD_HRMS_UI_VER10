using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class Form12LabourReport
    {
        public UserBase User { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public Designation Designation { get; set; }
        public int?[] Designations { get; set; }
        public Department Department { get; set; }
        public int?[] Departments { get; set; }
        public Location Location { get; set; }
        public int?[] Locations { get; set; }
        public Category Category { get; set; }
        public int?[] Categories { get; set; }
        public Grade Grade { get; set; }
        public int?[] Grades { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public int?[] PayrollGroups { get; set; }
        public Division Division { get; set; }
        public int?[] Divisions { get; set; }
        public Section Section { get; set; }
        public int?[] Sections { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int?[] EmploymentTypes { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public int?[] WorkLocations { get; set; }
        public WageGradeMaster WageGradeMaster { get; set; }
        public int?[] WageGrades { get; set; }
        public Region Region { get; set; }
        public int?[] Regions { get; set; }
        public State State { get; set; }
        public int?[] States { get; set; }


    }
}
