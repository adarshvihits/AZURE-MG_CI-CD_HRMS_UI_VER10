using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class DocumentUpload
    {        
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }

        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Update { get; set; }
        public int NotUpdate { get; set; }
        public bool Aadhar { get; set; }
        public bool PAN { get; set; }
        public bool DrivingLicense { get; set; }
        public bool Passport { get; set; }
        public bool Others { get; set; }
        public int AadharDocID { get; set; }
        public string NameInAadhar { get; set; }
        public string AadharNumber { get; set; }
        public DateTime? AadharIssueDate { get; set; }
        public int PANDocID { get; set; }
        public string NameInPAN { get; set; }
        public string PANNumber { get; set; }
        public DateTime? PANIssueDate { get; set; }
        public int DrivingLicenseDocID { get; set; }
        public string NameInDrivingLicense { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime? DrivingLicenseValidFrom { get; set; }
        public DateTime? DrivingLicenseValidTo { get; set; }
        public int PassportDocID { get; set; }
        public string NameInPassport { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportValidFrom { get; set; }
        public DateTime? PassportValidTo { get; set; }
        public Guid SliderDocumentGI { get; set; }
        public Guid SliderDocumentLogGI { get; set; }
        public string SliderDocumentNo { get; set; }
        public string SliderDocumentName { get; set; }
        public DocType SliderDocumentType { get; set; }
        public DocType SliderDocument { get; set; }
        public DateTime? SliderDateFrom { get; set; }
        public DateTime? SliderDateTo { get; set; }
        public string SliderDescription { get; set; }
        public String Docurl { get; set; }
        public string DocFileName { get; set; }
        public List<OtherDocList> OtherDocList { get; set; }
        public List<DocumentUpload> DocumentUploadList { get; set; }
        public File File { get; set; }
        public string SliderTitle { get; set; }
        public string Mode { get; set; }
        public FilterSlabComboFill FilterCombo { get; set; }
        public Filter Filter { get; set; }
        public string RefNo { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string SslcDescription { get; set; }
        public string HseDescription { get; set; }
        public string GraduationDescription { get; set; }
        public string PgDescription { get; set; }
        public string SslcStatus { get; set; }
        public string HseStatus { get; set; }
        public string GraduationStatus { get; set; }
        public string PgStatus { get; set; }
        public string AadhaarStatus { get; set; }
        public string PanStatus { get; set; }
        public string PassportStatus { get; set; }
        public string DLStatus { get; set; }
        public string docname { get; set; }
        public string BreadCrumb { get; set; }
        public string BreadCrumbName { get; set; }
        public string TabName { get; set; }
        public string AadharUrl { get; set; }
        public string AadharFileName { get; set; }
        public string AadharFile { get; set; }
        public string PanUrl { get; set; }
        public string PanFileName { get; set; }
        public byte[] PanFile { get; set; }

        public string PassportUrl { get; set; }
        public string PassportFileName { get; set; }

        public byte[] PassportFile { get; set; }
        public string DLUrl { get; set; }
        public string DLFileName { get; set; }
        public byte[] DLFile { get; set; }

    }
    public class DocType : MasterBase
    {

    }
    public class OtherDocList
    {
        public Guid EmployeeOtherDocumentGI { get; set; }
        public Guid LogGI { get; set; }
        public int EmployeeOtherDocumentId { get; set; }
        public string OtherDocumentName { get; set; }
        public string OtherDocumentType { get; set; }
        public string OtherDocumentDescription { get; set; }
        public DateTime? OtherDocumentDate { get; set; }
        public string OtherDocumentUrl { get; set; }
        public string OtherDocumentFileName { get; set; }
    }

    public class PanDocument
    {
        public string EmployeeCode { get; set; }

        public byte[] fdata { get; set; }

        public string FileName { get; set; }
    }
}

