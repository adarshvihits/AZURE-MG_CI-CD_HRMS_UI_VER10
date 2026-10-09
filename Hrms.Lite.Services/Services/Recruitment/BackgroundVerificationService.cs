using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using awsfile = Hrms.Lite.Shared.File;
using System.Net.Http;

namespace Hrms.Lite.Services.Services.Recruitment
{
   public class BackgroundVerificationService : IBackgroundVerificationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public BackgroundVerificationService(IServiceBase httpService)
        {
            _baseURL = "BackgroundVerification";  
            _httpService = httpService;
        }

        public async Task<BackgroundVerification> GetCandidateDetails(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<BackgroundVerification>($"{_baseURL}/GetCandidateDetails?PreEnrollGI={PreEnrollGI}");
        }
        
        public async Task<ResponseEntity> SaveBackgroundVerification(BackgroundVerification model)
        {
            return await _httpService.PostAsync<ResponseEntity, BackgroundVerification>($"{_baseURL}/SaveBackgroundVerification", model);
        }
        public async Task<ResponseEntity> BackgroundVerificationDetailsSave(BackgroundVerification model)
        {
            return await _httpService.PostAsync<ResponseEntity, BackgroundVerification>($"{_baseURL}/BackgroundVerificationDetailsSave", model);
        }

    }
}
