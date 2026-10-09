using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public class OfferProcessingService : IOfferProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OfferProcessingService(IServiceBase httpService)
        {
            _baseURL = "OfferProcessing";
            _httpService = httpService;
        }
        public async Task<OnBoarding> GetOfferProcessingList(int TabIndex, Filter Filter, string TabName)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetOfferProcessingList?TabIndex={TabIndex}&TabName={TabName}", Filter);
        }
        public async Task<OnBoarding> GetInitiationList(string TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetInitiationList?TabIndex={TabIndex}", Filter);
        }
        public async Task<PreEnrollment> GetOfferProcessingDetails(Guid ResumeDataBankGI, int ApplicantTrackingCode)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetOfferProcessingDetails?ResumeDataBankGI={ResumeDataBankGI}&ApplicantTrackingCode={ApplicantTrackingCode}");
        }

        public async Task<PreEnrollment> GetCandidateDetails(Guid PreEnrollGI)
        {
           return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetCandidateDetails?PreEnrollGI={PreEnrollGI}");
        }

        public async Task<PreEnrollment> GetOfferInitiationDetails( Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetOfferInitiationDetails?PreEnrollGI={PreEnrollGI}");
        }

        public async Task<PreEnrollment> GetOfferLetterDetails(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetOfferLetterDetails?PreEnrollGI={PreEnrollGI}");
        }

        public async Task<ResponseEntity> OfferInitiation(PreEnrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity,PreEnrollment>($"{_baseURL}/OfferInitiation", model);
        }
       public async Task<ResponseEntity> RepotService(int Code)
        {
            return await _httpService.GetReportAsync<ResponseEntity>($"/GenerateOfferLetter?PreEnrollCode={Code}");
        }
        public async Task<ResponseEntity> SaveCreditCheck(CreditEmployeementChecking CreditChecking)
        {
            return await _httpService.PostAsync<ResponseEntity, CreditEmployeementChecking>($"{_baseURL}/SaveCreditCheck", CreditChecking);
        }

        public async Task<ResponseEntity> SaveEmployementCheck(CreditEmployeementChecking EmployementChecking)
        {
            return await _httpService.PostAsync<ResponseEntity, CreditEmployeementChecking>($"{_baseURL}/SaveEmployementCheck", EmployementChecking);
        }

        public async Task<ResponseEntity<string>> SaveOfferProcessing(PreEnrollment preEnrollment)
        {
            return await _httpService.PostAsync<ResponseEntity<string>, PreEnrollment>($"{_baseURL}/SaveOfferProcessing", preEnrollment);
        }

        public async Task<PreEnrollment> PreviewOfferProcessing(PreEnrollment preEnrollment)
        {
            return await _httpService.PostAsync<PreEnrollment, PreEnrollment>($"{_baseURL}/PreviewOfferProcessing", preEnrollment);                    
        }
        public async Task<ResponseEntity> OfferAcceptance(PreEnrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"JobPortal_ApplicantProfile/OfferAcceptance", model);
        }
        public async Task<Enrollment> SalaryDetails_enroll(Enrollment Enrollment)
        {
            return await _httpService.PostAsync<Enrollment, Enrollment>($"{_baseURL}/SalaryDetails_enroll", Enrollment);
        }
    }
}
