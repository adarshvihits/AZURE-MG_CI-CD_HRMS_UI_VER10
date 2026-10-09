using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class FilterMultiSelect
    {

        public Basic_MultiFilter Basic_Filter { get; set; }
        public Advance_MultiFilter Advance_Filter { get; set; }

        public Optional_MultiFilter Optional_Filter { get; set; }

        public Date_FilterTest Date_Filter { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public Month F_Month { get; set; }
        public PayrollGroup F_PayrollGroup { get; set; }
        public Circle F_Circle { get; set; }
        public DateTime F_From { get; set; }
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

    public class Basic_MultiFilter
    {
        public int?[] Designation_Code { get; set; }
        public string[] Designation_Name { get; set; }
        public SelectList DesignationDropdownData { get; set; }
        public int?[] Department_Code { get; set; }
        public string[] Department_Name { get; set; }
        public SelectList DepartmentDropdownData { get; set; }
        public int?[] Location_Code { get; set; }
        public string[] Location_Name { get; set; }
        public SelectList LocationDropdownData { get; set; }
        public int?[] Category_Code { get; set; }
        public string[] Category_Name { get; set; }
        public SelectList CategoryDropdownData { get; set; }
        public int?[] Grade_Code { get; set; }
        public string[] Grade_Name { get; set; }
        public SelectList GradeDropdownData { get; set; }
        public int?[] PayrollGroup_Code { get; set; }
        public string[] PayrollGroup_Name { get; set; }
        public SelectList PayrollGroupDropdownData { get; set; }
        public int?[] Region_Code { get; set; }
        public string[] Region_Name { get; set; }
        public SelectList RegionDropdownData { get; set; }
        public int?[] State_Code { get; set; }
        public string[] State_Name { get; set; }
        public SelectList StateDropdownData { get; set; }
    }
    public class Optional_MultiFilter
    {
        public int?[] Division_Code { get; set; }
        public string[] Division_Name { get; set; }
        public SelectList DivisionDropdownData { get; set; }
        public int?[] Section_Code { get; set; }
        public string[] Section_Name { get; set; }
        public SelectList SectionDropdownData { get; set; }
        public int?[] EmploymentType_Code { get; set; }
        public string[] EmploymentType_Name { get; set; }
        public SelectList EmploymentTypeDropdownData { get; set; }
        public int?[] WorkLocation_Code { get; set; }
        public string[] WorkLocation_Name { get; set; }
        public SelectList WorkLocationDropdownData { get; set; }
        public int?[] WageGradeMaster_Code { get; set; }
        public string[] WageGradeMaster_Name { get; set; }
        public SelectList WageGradeMasterDropdownData { get; set; }
        public int?[] EmploymentStatus_Code { get; set; }
        public string[] EmploymentStatus_Name { get; set; }
        public SelectList EmploymentStatusDropdownData { get; set; }
        public int?[] Gender_Code { get; set; }
        public string[] Gender_Name { get; set; }
        public SelectList GenderDropdownData { get; set; }
    }



    public class Advance_MultiFilter
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

    public class Date_FilterTest
    {
        public DateType DateType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }



    public class EmploymentStatusTest : MasterBase
    {

    }

    public class DateTypeTest : MasterBase
    {

    }
}
