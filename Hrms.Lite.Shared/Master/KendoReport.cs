using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class KendoReport : MasterBase
    {

        public int EmployeeCode { get; set; }

        public int ResignationCode { get; set; }

        public int RequestCode { get; set; }
        public int EmpCode { get; set; }
        public int DocumentTypeCode { get; set; }
        public int TypeCode { get; set; }
        public int GcCode { get; set; }
        public int GroupcompanyCode { get; set; }
        public string Reason { get; set; }
        public int UserCode { get; set; }
        public int Console { get; set; }
        public int TrnCode { get; set; }


        public int PreEnrollCode { get; set; }
        public Guid GroupCompanyGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public int HistoryCode { get; set; }
        public string type { get; set; }
        public Url Url { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int FormatCode { get; set; }
        public bool flag { get; set; }
        public string Date { get; set; }
        public string PromotionType { get; set; }
        public string Remarks { get; set; }
        public string offerletterNum { get; set; }
        public string offerletterdate { get; set; }
        public string ActionDate { get; set; }
    }
}
