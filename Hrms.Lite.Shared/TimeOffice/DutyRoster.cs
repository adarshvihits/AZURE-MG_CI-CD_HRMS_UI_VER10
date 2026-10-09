using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Hrms.Lite.Shared.TimeOffice
{
    public class DutyRoster
    {
        public int EmpId { get; set; }
        public Guid EmployeeGI { get; set; }
        public UserBase User { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Remark { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Month Month { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public Dropdown ShiftType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public DateTime? ImportFrom { get; set; }
        public DateTime? ImportTo { get; set; }
        public DateTime? Batch_From { get; set; }
        public DateTime? Batch_To { get; set; }
        public Circle Circle { get; set; }
        public int TotalRows { get; set; }
        public ShiftDetails ShiftDetails { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public DutyRoster_MainList MainList { get; set; }
        public Filter Filter { get; set; }
        public List<dynamic> Data { get; set; }
        public List<Employee_DutyRosterEditDetails> Employee_DutyRosterEditDetails { get; set; }
        public List<DateWise_DutyRosterEditDetails> DateWise_DutyRosterEditDetails { get; set; }

        public List<DutyRoster_EmployeePrimaryInfo> DutyRoster_EmployeePrimaryInfo { get; set; }
        public List<DutyRosterList> DutyRosterList { get; set; }
        public DutyRosterFooterList DutyRosterFooterList { get; set; }
        public DateTime? SelectedDate { get; set; }
    }
    public class DutyRoster_MainList
    {
        public List<Header> Header { get; set; }
        public List<DutyRoster_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public List<ShiftDayDetails> ShiftDayDetails { get; set; }

    }
    public class DutyRoster_EmployeePrimaryInfo
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public Designation Designation { get; set; }
        public bool Applicability { get; set; }

    }
    public class ShiftDayDetails
    {
        public int ShiftCode { get; set; }
        public string Shift { get; set; }

    }
    public class Employee_DutyRosterEditDetails
    {
        public Guid EmployeeGI { get; set; }
        public Guid DutyRosterGI { get; set; }
        public int DutyRosterCode { get; set; }
        public Month Month { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public ShiftDetails ShiftDetails { get; set; }


    }
    public class DateWise_DutyRosterEditDetails
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public Guid DutyRosterGI { get; set; }
        public int DutyRosterCode { get; set; }
        public Month Month { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public ShiftDetails ShiftDetails { get; set; }


    }
    public class DutyRosterList
    {

        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
          public string Remark { get; set; }
        public int TotalRows { get; set; }
        public int ORDER { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public string Day11 { get; set; }
        public string Day12 { get; set; }
        public string Day13 { get; set; }
        public string Day14 { get; set; }
        public string Day15 { get; set; }
        public string Day16 { get; set; }
        public string Day17 { get; set; }
        public string Day18 { get; set; }
        public string Day19 { get; set; }
        public string Day20 { get; set; }
        public string Day21 { get; set; }
        public string Day22 { get; set; }
        public string Day23 { get; set; }
        public string Day24 { get; set; }
        public string Day25 { get; set; }
        public string Day26 { get; set; }
        public string Day27 { get; set; }
        public string Day28 { get; set; }
        public string Day29 { get; set; }
        public string Day30 { get; set; }
        public string Day31 { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public string Day1_Date { get; set; }
        public string Day2_Date { get; set; }
        public string Day3_Date { get; set; }
        public string Day4_Date { get; set; }
        public string Day5_Date { get; set; }
        public string Day6_Date { get; set; }
        public string Day7_Date { get; set; }
        public string Day8_Date { get; set; }
        public string Day9_Date { get; set; }
        public string Day10_Date { get; set; }
        public string Day11_Date { get; set; }
        public string Day12_Date { get; set; }
        public string Day13_Date { get; set; }
        public string Day14_Date { get; set; }
        public string Day15_Date { get; set; }
        public string Day16_Date { get; set; }
        public string Day17_Date { get; set; }
        public string Day18_Date { get; set; }
        public string Day19_Date { get; set; }
        public string Day20_Date { get; set; }
        public string Day21_Date { get; set; }
        public string Day22_Date { get; set; }
        public string Day23_Date { get; set; }
        public string Day24_Date { get; set; }
        public string Day25_Date { get; set; }
        public string Day26_Date { get; set; }
        public string Day27_Date { get; set; }
        public string Day28_Date { get; set; }
        public string Day29_Date { get; set; }
        public string Day30_Date { get; set; }
        public string Day31_Date { get; set; }
        public string WorkHours { get; set; }
        public string AvgHours { get; set; }
        public int NightShift { get; set; }
        public int MorningShift { get; set; }
        public int TotalShift { get; set; }
        public int TotalOffday { get; set; }
    }
    public class DutyRosterFooterList
    {

        public string Day1_Footer { get; set; }
        public string Day2_Footer { get; set; }
        public string Day3_Footer { get; set; }
        public string Day4_Footer { get; set; }
        public string Day5_Footer { get; set; }
        public string Day6_Footer { get; set; }
        public string Day7_Footer { get; set; }
        public string Day8_Footer { get; set; }
        public string Day9_Footer { get; set; }
        public string Day10_Footer { get; set; }
        public string Day11_Footer { get; set; }
        public string Day12_Footer { get; set; }
        public string Day13_Footer { get; set; }
        public string Day14_Footer { get; set; }
        public string Day15_Footer { get; set; }
        public string Day16_Footer { get; set; }
        public string Day17_Footer { get; set; }
        public string Day18_Footer { get; set; }
        public string Day19_Footer { get; set; }
        public string Day20_Footer { get; set; }
        public string Day21_Footer { get; set; }
        public string Day22_Footer { get; set; }
        public string Day23_Footer { get; set; }
        public string Day24_Footer { get; set; }
        public string Day25_Footer { get; set; }
        public string Day26_Footer { get; set; }
        public string Day27_Footer { get; set; }
        public string Day28_Footer { get; set; }
        public string Day29_Footer { get; set; }
        public string Day30_Footer { get; set; }
        public string Day31_Footer { get; set; }
    }
}
