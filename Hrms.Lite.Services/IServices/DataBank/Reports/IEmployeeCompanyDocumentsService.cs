using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEmployeeCompanyDocumentsService
    {
        //public Task<EmployeeCompanyDocuments> GetExperienceletterbracnhesDetails(int EmployeeCode, int LetterFormateCode);

        public Task<EmployeeCompanyDocuments> GetEmployeeCompanyList(int DocTypeCode, string FromDate,string Todate,Filter filter);
        public Task<EmployeeCompanyDocuments> GetNoDueEmpList(int DocTypeCode, int FormatCode, string FromDate, string Todate, Filter filter);
        public Task<DataSet> GetEmployeeCompanyDocumentsList_Excel(string From, string To, int DocumentType, Filter filterdata);
        public Task<AppointmentLetter> AppointmentList(int DocTypeCode,int DocumentFormatCode, string FromDate, string Todate,string Type, Filter filter);
        public Task<OfferLetter> GetofferLetterList(string TabIndex,int DocTypeCode, string Type, Filter filter);
        public Task<EmployeeCompanyDocuments> SalaryCertificateList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);
        public Task<ExperienceLetter> GetExperienceLetter(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);
        public Task<RelievingLetter> GetRelievingLetterList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter);
        public Task<FTCRevisionLetter> GetFTCRevisionList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode,string Type, Filter filter);
        public Task<AppointmentLetter> GetProbationExtensionLetterList(string FromDate, string Todate, int DocTypeCode,int HisCode, int DocumentFormatCode, string Type, Filter filter);

        public Task<PromotionLetter> GetPromotionList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);
        public Task<IncrementLetter> GetIncrementList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);
        public Task<EmployeeCompanyDocuments> GetTransferList(int DocTypeCode,int DocumentFormatCode,string FromDate, string Todate, Filter filter);
        public Task<EmployeeCompanyDocuments> GetWarningList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter);
        public Task<EmployeeCompanyDocuments> GetShowCauseEmpList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter);

        
        public Task<EmployeeCompanyDocuments> GetSalaryEmpList(Filter filter);
        public Task<EmployeeCompanyDocuments> GetConfirmationList(string FromDate, string Todate, Filter filter);
        public Task<Termination> GetTerminationList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter);
        public Task<EmploymentCertificate> GetEmploymentCertificate(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter);
        public Task<EmployeeCompanyDocuments> GetDeploymentList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter);
        public Task<StillWorkingLetter> StillWorkingList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);
        public Task<AbscondingLetter> AbscondingLetterList(int DocTypeCode, int DocumentFormatCode, string Type, Filter filter);



    }
}
