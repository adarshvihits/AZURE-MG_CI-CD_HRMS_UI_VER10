using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class WageGradeVsSumAssured
    {
        public int WageGradeCode { get; set; }
        public string WageGrade { get; set; }
        public decimal MediClaim { get; set; }
        public decimal PersonalAccident { get; set; }
        public decimal GTLI { get; set; }
        public List<WageGradeVsSumAssured> WageGradeVsSumAssuredList { get; set; }
    }
   
}
