using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.CandidateLink;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class CandidatePreEnrolLinkService: ICandidatePreEnrolLinkService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CandidatePreEnrolLinkService(IServiceBase httpService)
        {
            _baseURL = "PreenrollCandidateLink";
            _httpService = httpService;
        }

       public async Task<IList<PreEnrollCandidateLink>> GetPreEnrollLinkApprovalList(Filter filterdata)
        {
            return await _httpService.PostAsync<IList<PreEnrollCandidateLink>, Filter>($"{_baseURL}/GetPreEnrollLinkApprovalList", filterdata);
        }

        public async Task<PersonalDetails> GetPreEnrollLinkApprovalData(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPreEnrollLinkApprovalData?PreEnrollGI={PreEnrollGI}");
        }
        public async Task<ResponseEntity> ApprovePreEnrollLinkDetails(PersonalDetails PersonalDetails)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetails>($"{_baseURL}/ApprovePreEnrollLinkDetails", PersonalDetails);
        }
        public async Task<PersonalDetails> GetCandidateList(string TabIndex, Filter Filter, int radiomode)
        {
            return await _httpService.PostAsync<PersonalDetails, Filter>($"{_baseURL}/GetCandidateListDetails?TabIndex={TabIndex}&radiomode={radiomode}", Filter);
        }
        public async Task<ResponseEntity> CandidateLinkSend(PersonalDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetails>($"{_baseURL}/CandidateLinkSend", model);
        }
        public async Task<ResponseEntity> EditCandidateEmail(PersonalDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetails>($"{_baseURL}/EditCandidateEmail", model);
        }
    }
}
