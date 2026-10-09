using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class DocumentVerificationService : IDocumentVerificationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DocumentVerificationService(IServiceBase httpService)
        {
            _baseURL = "DocumentVerification";
            _httpService = httpService;
        }
        public async Task<DocumentVerification> GetDocumentVerificationList()
        {
            return await _httpService.GetAsync<DocumentVerification>($"{_baseURL}/GetDocumentVerificationList");
        }
        public async Task<DocumentVerification> GetOtherDocumentList(Guid ApplicantGI)
        {
            return await _httpService.GetAsync<DocumentVerification>($"{_baseURL}/GetOtherDocumentList?ApplicantGI={ApplicantGI}");
        }
        public async Task<DocumentVerification> GetDocumentVerificationDetails(Guid VacancyGI, int CandidateCode, int ApplicantTrackingCode)
        {
            return await _httpService.GetAsync<DocumentVerification>($"{_baseURL}/GetDocumentVerificationDetails?VacancyGI={VacancyGI}&CandidateCode={CandidateCode}&ApplicantTrackingCode={ApplicantTrackingCode}");
        }
        public async Task<DocumentVerification> GetIdentityDocumentDetails(Guid ApplicantGI, string DOC_TYPE)
        {
            return await _httpService.GetAsync<DocumentVerification>($"{_baseURL}/GetIdentityDocumentDetails?ApplicantGI={ApplicantGI}&DOC_TYPE={DOC_TYPE}");
        }
        public async Task<DocumentVerification> GetOtherDocumentDetails(string OtherDocName,Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DocumentVerification>($"{_baseURL}/GetOtherDocumentDetails?OtherDocName={OtherDocName}&EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> Verify(DocumentVerification model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentVerification>($"{_baseURL}/Verify",model);
        }
        public async Task<ResponseEntity> ApplicantDocumentUploadSave(DocumentVerification model, string DOC_TYPE)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentVerification>($"{_baseURL}/ApplicantDocumentUploadSave?DocType={DOC_TYPE}",model);
        }

        public async Task<ResponseEntity> DocumentApprove(DocumentVerification model, string DOC_TYPE, int ApplicantTrackingCode)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentVerification>($"{_baseURL}/DocumentApprove?DocType={DOC_TYPE}&ApplicantTrackingCode={ApplicantTrackingCode}",model);
        }

        public async Task<ResponseEntity> ApplicantOtherDocumentUploadSave(DocumentVerification model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentVerification>($"{_baseURL}/ApplicantOtherDocumentUploadSave",model);
        }
        public async Task<ResponseEntity> IdentityDocumentDelete(Guid ApplicantGI, string DocType)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/IdentityDocumentUploadDelete?EmployeeGI={ApplicantGI}&DocType={DocType}");
        }
        public async Task<ResponseEntity> OtherDocumentDelete(Guid OtherDocGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/OtherDocumentUploadDelete?OtherDocGI={OtherDocGI}");
        }
    }
}
