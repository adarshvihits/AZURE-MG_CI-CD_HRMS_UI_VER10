using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
   public class TDS24Q
    {
        public UserBase User { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public int?[] Designations { get; set; }

        public int?[] Departments { get; set; }

        public int?[] Locations { get; set; }

        public int?[] Categories { get; set; }

        public int?[] Grades { get; set; }

        public int?[] PayrollGroups { get; set; }

        public int?[] Divisions { get; set; }

        public int?[] Sections { get; set; }

        public int?[] EmploymentTypes { get; set; }

        public int?[] WorkLocations { get; set; }

        public int?[] WageGrades { get; set; }
        public int BasedOn { get; set; }
    }
}
