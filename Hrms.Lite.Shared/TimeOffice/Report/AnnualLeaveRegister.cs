using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class AnnualLeaveRegister
    {
        public List<AnnualLeaveRegisterPaging> AnnualLeaveRegisterPagingList { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown FinancialYear { get; set; }
        public Dropdown Employee { get; set; }
        public Filter Filter { get; set; }
        public string countSummary { get; set; }
    }
    public class AnnualLeaveRegisterPaging
    {

        public int EmployeeCode { get; set; }
        public int MonthCode { get; set; }
        public string MonthName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
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
        public string Day1_Color { get; set; }
        public string Day2_Color { get; set; }
        public string Day3_Color { get; set; }
        public string Day4_Color { get; set; }
        public string Day5_Color { get; set; }
        public string Day6_Color { get; set; }
        public string Day7_Color { get; set; }
        public string Day8_Color { get; set; }
        public string Day9_Color { get; set; }
        public string Day10_Color { get; set; }
        public string Day11_Color { get; set; }
        public string Day12_Color { get; set; }
        public string Day13_Color { get; set; }
        public string Day14_Color { get; set; }
        public string Day15_Color { get; set; }
        public string Day16_Color { get; set; }
        public string Day17_Color { get; set; }
        public string Day18_Color { get; set; }
        public string Day19_Color { get; set; }
        public string Day20_Color { get; set; }
        public string Day21_Color { get; set; }
        public string Day22_Color { get; set; }
        public string Day23_Color { get; set; }
        public string Day24_Color { get; set; }
        public string Day25_Color { get; set; }
        public string Day26_Color { get; set; }
        public string Day27_Color { get; set; }
        public string Day28_Color { get; set; }
        public string Day29_Color { get; set; }
        public string Day30_Color { get; set; }
        public string Day31_Color { get; set; }
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
    }
}
