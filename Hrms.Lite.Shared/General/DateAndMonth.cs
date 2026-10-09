using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class DateAndMonth
    {
        public int MonthCode { get; set; }
        public int YearCode { get; set; }
        public int FyCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? NextMonthCode { get; set; }
        public int? Processed_NextMonthCode { get; set; }
        public int? PreviousMonthCode { get; set; }
        public int? Processed_PreviousMonthCode { get; set; }
        public int? LastProcessedMonth { get; set; }
    }
}
