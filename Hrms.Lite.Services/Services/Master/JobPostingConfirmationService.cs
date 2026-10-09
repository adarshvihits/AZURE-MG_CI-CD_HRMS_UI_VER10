using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class JobPostingConfirmationService : IJobPostingConfirmationService
    {
        private readonly string _baseURL;

        private readonly IServiceBase _httpService;
        public JobPostingConfirmationService(IServiceBase httpService)
        {
            _baseURL = "JobPostingConfirmation";
            _httpService = httpService;
        }

        public async Task<JobPostingConfirmationViewModel> GetJobPostingConfirmationList(string TabIndex)
        {
            return await _httpService.GetAsync<JobPostingConfirmationViewModel>($"{_baseURL}/GetJobPostingConfirmationList?TabIndex={TabIndex}");
        }

        public async Task<JobPostingConfirmationDetails> GetVacancyDetails(int VacancySourceCode, int SourceTranactionCode)
        {
            return await _httpService.GetAsync<JobPostingConfirmationDetails>($"{_baseURL}/GetVacancyDetails?VacancySourceCode={VacancySourceCode}&SourceTranactionCode={SourceTranactionCode}");
        }

        public async Task<ResponseEntity> ConfirmPosting(JobPostingConfirmationRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobPostingConfirmationRequest>($"{_baseURL}/ConfirmPosting", model);
        }
    }
}
