using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class EmployeeCompanyDocumentsService : IEmployeeCompanyDocumentsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeCompanyDocumentsService(IServiceBase httpService)
        {
            _baseURL = "EmployeeCompanyDocuments";
            _httpService = httpService;
        }
        //public async Task<EmployeeCompanyDocuments> GetExperienceletterbracnhesDetails(int EmployeeCode, int LetterFormateCode)
        //{
        //    return await _httpService.GetAsync<EmployeeCompanyDocuments>($"{_baseURL}/GetExperienceletterbracnhesDetails?EmployeeCode={EmployeeCode}&LetterFormateCode={LetterFormateCode}");

        //}

        public async Task<EmployeeCompanyDocuments> GetEmployeeCompanyList(int DocTypeCode, string FromDate, string Todate, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetEmployeeCompanyDocumentsList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}", filterdata);
        }

        public async Task<DataSet> GetEmployeeCompanyDocumentsList_Excel(string From, string To, int DocumentType, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeCompanyDocumentsList_Excel?From={From}&To={To}&DocumentType={DocumentType}", filterdata);

        }

        public async Task<AppointmentLetter> AppointmentList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate,string Type, Filter filter)
        {
            return await _httpService.PostAsync<AppointmentLetter, Filter>($"{_baseURL}/GetAppointmentLetterList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<EmployeeCompanyDocuments> GetTransferList(int DocTypeCode,int DocumentFormatCode,string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetTransferEmpList?FromDate={FromDate}&ToDate={Todate}&DocTypeCode={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);

        }

        public async Task<EmployeeCompanyDocuments> GetSalaryEmpList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetSalaryEmpList", filter);
        }

        public async Task<ExperienceLetter> GetExperienceLetter(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<ExperienceLetter, Filter>($"{_baseURL}/GetExperienceLetterList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);


        }

        public async Task<FTCRevisionLetter> GetFTCRevisionList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode,string Type, Filter filter)
        {
            return await _httpService.PostAsync<FTCRevisionLetter, Filter>($"{_baseURL}/GetFTCRevisionList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);

        }

        public async Task<PromotionLetter> GetPromotionList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<PromotionLetter, Filter>($"{_baseURL}/GetPromotionList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<AppointmentLetter> GetProbationExtensionLetterList(string FromDate, string Todate,int HisCode, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<AppointmentLetter, Filter>($"{_baseURL}/GetProbationExtensionLetterList?From={FromDate}&To={Todate}&HisCode={HisCode}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<IncrementLetter> GetIncrementList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<IncrementLetter, Filter>($"{_baseURL}/GetIncrementLetterList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<RelievingLetter> GetRelievingLetterList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter)
        {
            return await _httpService.PostAsync<RelievingLetter, Filter>($"{_baseURL}/GetRelievingLetterList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);
        }

        public async Task<EmployeeCompanyDocuments> SalaryCertificateList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetSalaryEmpList?FromDate={FromDate}&Todate={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<OfferLetter> GetofferLetterList(string TabIndex, int DocTypeCode,string Type, Filter filter)
        {
            return await _httpService.PostAsync<OfferLetter, Filter>($"{_baseURL}/GetOfferLetterListDetails?TabIndex={TabIndex}&DocTypeCode={DocTypeCode}&Type={Type}", filter);
        }
        public async Task<EmployeeCompanyDocuments> GetConfirmationList(string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetConfirmationEmpList?FromDate={FromDate}&ToDate={Todate}", filter);

        }

        public async Task<Termination> GetTerminationList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter)
        {
            return await _httpService.PostAsync<Termination, Filter>($"{_baseURL}/GetTerminationList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);

        }

        public async Task<EmploymentCertificate> GetEmploymentCertificate(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, Filter filter)
        {
            return await _httpService.PostAsync<EmploymentCertificate, Filter>($"{_baseURL}/GetEmploymentCertificateList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);
        }

        public async Task<EmployeeCompanyDocuments> GetWarningList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetWarningList?FromDate={FromDate}&ToDate={Todate}&DocTypeCode={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);

        }

        public async Task<EmployeeCompanyDocuments> GetShowCauseEmpList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetShowCauseEmpList?FromDate={FromDate}&ToDate={Todate}&DocTypeCode={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);

        }

        public async Task<EmployeeCompanyDocuments> GetDeploymentList(int DocTypeCode, int DocumentFormatCode, string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetDeploymentList?FromDate={FromDate}&ToDate={Todate}&DocTypeCode={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}", filter);
        }
        public async Task<EmployeeCompanyDocuments> GetNoDueEmpList(int DocTypeCode, int FormatCode, string FromDate, string Todate, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeCompanyDocuments, Filter>($"{_baseURL}/GetNoDueEmpList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={FormatCode}", filter);

        }
        
        public async Task<StillWorkingLetter> StillWorkingList(string FromDate, string Todate, int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<StillWorkingLetter, Filter>($"{_baseURL}/GetStillWorkingLetterList?From={FromDate}&To={Todate}&DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }

        public async Task<AbscondingLetter> AbscondingLetterList( int DocTypeCode, int DocumentFormatCode, string Type, Filter filter)
        {
            return await _httpService.PostAsync<AbscondingLetter, Filter>($"{_baseURL}/GetAbscondingLetterList?DocumentType={DocTypeCode}&DocumentFormatCode={DocumentFormatCode}&Type={Type}", filter);
        }
    }
}
