using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
   public class MultiSelectFilter
    {

        public Multi_Basic_Filter Basic_Filter { get; set; }
        public Advance_Filter Advance_Filter { get; set; }
        public Multi_Optional_Filter Optional_Filter { get; set; }   

        public Date_Filter Date_Filter { get; set; }

        public EmployeeWise_Filter EmployeeWise_Filter { get; set; }
        public string Search { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; }
        public int draw { get; set; }
    }


    public class EmployeeWise_Filter
    {
        public int EmployeeStatus { get; set; }
        public MultiSelectFilterDropDown Employee_ID_Name { get; set; }
        public SelectList DropdownData { get; set; }
    }
    public class Multi_Basic_Filter
    {
        public MultiSelectFilterDropDown Designation { get; set; }

        public MultiSelectFilterDropDown Department { get; set; }
        public MultiSelectFilterDropDown Location { get; set; }
        public MultiSelectFilterDropDown Category { get; set; }
        public MultiSelectFilterDropDown Grade { get; set; }
        public MultiSelectFilterDropDown PayrollGroup { get; set; }
        public MultiSelectFilterDropDown Region { get; set; }
        public MultiSelectFilterDropDown State { get; set; }

    }
    public class Multi_Optional_Filter
    {
        public MultiSelectFilterDropDown Division { get; set; }
        public MultiSelectFilterDropDown Section { get; set; }
        public MultiSelectFilterDropDown EmploymentType { get; set; }
        public MultiSelectFilterDropDown WorkLocation { get; set; }
        public MultiSelectFilterDropDown WageGradeMaster { get; set; }
        public MultiSelectFilterDropDown EmploymentStatus { get; set; }
        public MultiSelectFilterDropDown Gender { get; set; }
    }


    public class  MultiSelectFilterDropDown
    {
        public int?[] Values { get; set; }
        public string[] Texts { get; set; }      
    }
   

}
