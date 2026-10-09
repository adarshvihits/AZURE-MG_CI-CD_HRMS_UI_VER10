using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class ApplicantTrackingService: IApplicantTrackingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ApplicantTrackingService(IServiceBase httpService)
        {
            _baseURL = "ApplicantTracking";
            _httpService = httpService;
        }
        public async Task<ApplicantTracking> GetList(int Tabindex)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetResumeDataBankList?Tabindex={Tabindex}");
        }
        public async Task<ApplicantTracking> GetShortListedDetails(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetShortListedDetails?ResumeDataBankGI={ResumeDataBankGI}");
        }
        public async Task<ApplicantTracking_Details> GetInterviewEvaluationDetails(Guid ResumeDataBankGI, string InterviewStatusType)
        {
            return await _httpService.GetAsync<ApplicantTracking_Details>($"{_baseURL}/GetInterviewEvaluationDetails?ResumeDataBankGI={ResumeDataBankGI}&InterviewStatusType={InterviewStatusType}");
        }
        public async Task<ApplicantTracking_Details> GetEvaluationDetails(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ApplicantTracking_Details>($"{_baseURL}/GetInterviewEvaluationDetails?ResumeDataBankGI={ResumeDataBankGI}");
        }
        public async Task<ApplicantTracking> GetInterviewEvaluationView(Guid ResumeDataBankGI, string InterviewStatusType)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetInterviewEvaluationView?ResumeDataBankGI={ResumeDataBankGI}&InterviewStatusType={InterviewStatusType}");
        }
        public async Task<ApplicantTracking> GetApplicantTracker(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetApplicantTracker?ResumeDataBankGI={ResumeDataBankGI}");
        }
        public async Task<ResponseEntity> ScreeningSave(ApplicantTracking model)
        { 
            return await _httpService.PostAsync<ResponseEntity,ApplicantTracking>($"{_baseURL}/ScreeningSave",model);
        }
        public async Task<ResponseEntity> InterviewEvaluationSave(ApplicantTracking model)
        { 
            return await _httpService.PostAsync<ResponseEntity, ApplicantTracking>($"{_baseURL}/InterviewEvaluationSave",model);
        }
        public async Task<ResponseEntity> FinalApproval(ApplicantTracking model)
        { 
            return await _httpService.PostAsync<ResponseEntity, ApplicantTracking>($"{_baseURL}/FinalApproval",model);
        }
        public async Task<ResponseEntity> HoldReject(ApplicantTracking model)
        { 
            return await _httpService.PostAsync<ResponseEntity, ApplicantTracking>($"{_baseURL}/HoldReject",model);
        }

        public async Task<ResumeDataBank> GetResumeDetails(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"{_baseURL}/GetResumeDetails?ResumeDataBankGI={ResumeDataBankGI}");
        }

        public async Task<IList<ApplicantTrackingListTabs>> GetRAApprovalListTabs()
        {
            return await _httpService.GetAsync<IList<ApplicantTrackingListTabs>> ($"{_baseURL}/GetRAApprovalListTabs");
        }

      public async Task<ResponseEntity> OfferReject(ApplicantTracking model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApplicantTracking>($"{_baseURL}/OfferReject", model);
        }
        public async Task<ApplicantTracking> GetInterviewDetails(int ApplicantTrackingCode)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetInterviewDetails?ApplicantTrackingCode={ApplicantTrackingCode}");
        }
        public async Task<ResponseEntity> OfferRevise(Guid PreEnrollGI, string Mode, string Remarks)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/OfferRevise?PreEnrollGI={PreEnrollGI}&Mode={Mode}&Remarks={Remarks}");
        }


    }
}
