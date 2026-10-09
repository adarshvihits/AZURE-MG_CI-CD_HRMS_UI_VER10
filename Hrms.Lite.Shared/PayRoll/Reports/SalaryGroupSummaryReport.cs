using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.DataBank.DTO;


namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class SalaryGroupSummaryReport
    {


        public MasterBase PayrollMonth { get; set; }

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

        public GroupByComboFill GroupByFill { get; set; }

    }

  
}

