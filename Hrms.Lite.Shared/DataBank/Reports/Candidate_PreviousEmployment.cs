using System;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class Candidate_PreviousEmployment
    {
        public int RowID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public Nullable<DateTime> Fromdate { get; set; }
        public Nullable<DateTime> Todate { get; set; }
        public Decimal? Experience { get; set; }
        public Decimal? AnualCTC { get; set; }
        public string ContanctPersonName { get; set; }
        public string ContanctPersonNumber { get; set; }
        public string ContanctPersonDesigantion { get; set; }
        public File ExCertificate { get; set; }
        public File PaySlip1 { get; set; }
        public File PaySlip2 { get; set; }
        public File PaySlip3 { get; set; }
    }
}