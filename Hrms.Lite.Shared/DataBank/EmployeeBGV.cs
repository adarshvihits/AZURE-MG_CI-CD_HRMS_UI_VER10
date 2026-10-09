using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeBGV
    {
        public Filter filter { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ApplicantCode { get; set; }
        public string ApplicantID { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string FuncState { get; set; }
        public DateTime DOJ { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int TabIndex { get; set; }
        public List<EmployeeBGV> EmployeeBGVList { get; set; }
        public int PendingCount { get; set; }
        public int CompletedCount { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Details Details { get; set; }
    }
    public class Details
    {
        public int ApplicantCode { get; set; }
        public string ApplicantID { get; set; }
        public string AadhaarNo { get; set; }
        public string NameInAadhaar { get; set; }
        public string PanNo { get; set; }
        public string WhatsappNo { get; set; }
        public bool Fresher { get; set; }
        public string RefereeName { get; set; }
        public string ContactInformation { get; set; }
        public string TitleAndCompanyName { get; set; }
        public MasterBase RelationshipWithCandidate { get; set; }
        public MasterBase HowManyYears { get; set; }
        public string DurationOFworkingTogether { get; set; }
        public MasterBase DurationYears { get; set; }
        public MasterBase DurationMonths { get; set; }
        public int TypeOfReference { get; set; }
        public string TypeOfReferenceName { get; set; }
        public string BGVCompanyName { get; set; }
        public string BGVCandidatePosition { get; set; }
        public string BGVRehire { get; set; }
        public string BGVReasonForExit { get; set; }
        public string OtherDetails { get; set; }
        public string Remarks { get; set; }
    }
}
