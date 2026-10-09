using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class AttritionReport
    {
        public List<string> Head { get; set; }
        public List<Rows> Rows { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int GroupBy { get; set; }
        public int Calculation { get; set; }
        public List<string> Footer { get; set; }
        public List<Rows> FooterRows { get; set; }
        public AttritionGrouping AttritionGrouping { get; set; }
        public AttritionCalculation AttritionCalculation { get; set; }
    }
    public class Rows
    {
        public List<string> Columns { get; set; }
    }
    public class AttritionGrouping : MasterBase
    {

    }
    public class AttritionCalculation : MasterBase
    {

    }
}
