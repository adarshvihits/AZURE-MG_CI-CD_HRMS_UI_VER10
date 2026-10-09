using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
   public class PreviousEmployment
    {
        public Guid PrevEmploymentLogGI { get; set; }
        public int PrevEmploymentLogCode { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public Nullable<DateTime> Fromdate { get; set; }
        public Nullable<DateTime> Todate { get; set; }
        public Decimal? Experience { get; set; }
        public bool ApprovedStatus { get; set; }
        public int flag { get; set; }
        public string ContanctPersonName { get; set; }
        public string ContanctPersonNumber { get; set; }
        public string ContanctPersonDesigantion { get; set; }
        public File File { get; set; }
    }
}
