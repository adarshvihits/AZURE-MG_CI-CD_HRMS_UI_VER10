using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class OfferAcceptanceReport
    {
        public List<OfferAcceptanceReport> OfferAcceptanceReportList { get; set; }
        public UserBase User { get; set; }
        public Filter Filter { get; set; }
        public int PreEnrollCode { get; set; }
        public string ApplicantID { get; set; }
        public string ApplicantName { get; set; }
        public string EmployeeID { get; set; }
        public string MRFNO { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime? OfferAcceptedDateAndTime { get; set; }
        public DateTime? OfferRejectedDateAndTime { get; set; }
        public string AcceptedBy { get; set; }
        public string RejectedBy { get; set; }
        public string OfferLetterAbsoluteUri { get; set; }
        public string OfferFileName { get; set; }
        public string OfferLetter { get; set; }
        public byte[]? OfferLetterData { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string RejectReason { get; set; }
        public string Stage { get; set; }
        public int TabIndex { get; set; }
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public int resigned { get; set; }

    }
}
