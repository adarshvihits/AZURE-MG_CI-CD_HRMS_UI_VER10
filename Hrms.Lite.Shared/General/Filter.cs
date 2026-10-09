using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace Hrms.Lite.Shared.General
{
    public class Filter
    {

        public Basic_Filter Basic_Filter { get; set; }
        public Advance_Filter Advance_Filter { get; set; }

        public Optional_Filter Optional_Filter { get; set; }

        public Date_Filter Date_Filter { get; set; }
        public EmployeeWise_Filter EmployeeWise_Filter { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public Month F_Month { get; set; }
        public PayrollGroup F_PayrollGroup { get; set; }
        public  Circle F_Circle { get; set; }
        public   DateTime F_From { get; set; }
        public DateTime F_To { get; set; }
        public string F_SelectedDate { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Search { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; }
        public int draw { get; set; }
        
    }

    public class Basic_Filter
    {
        public Designation Designation { get; set; }

        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public Grade Grade { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public State State { get; set; }
        public Region Region { get; set; }
        public ESIGroup ESIGroup { get; set; }
        public PFGroup PFGroup { get; set; }
    }
    public class Optional_Filter
    {
        public Division Division { get; set; }
        public Section Section { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public WageGradeMaster WageGradeMaster { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public Gender Gender { get; set; }
    }



    public class Advance_Filter
    {
        public string AgeAboveOrBelow { get; set; }
        public int AgeAboveBelow { get; set; }
        public string YSAboveOrBelow { get; set; }
        public int Age { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int YrsOfExperience { get; set; }
        public int YrsOfService { get; set; }

        public int YSFrom { get; set; }
        public int YSTo { get; set; }
        public SelectList Operators { get; set; }
        public SelectList DateType { get; set; }

    }

    public class Date_Filter
    {
        public DateType DateType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }



    public class EmploymentStatus  :MasterBase
    {
        public SelectList DropdownData { get; set; }
    }

    public class DateType : MasterBase
    { 

    }

}
