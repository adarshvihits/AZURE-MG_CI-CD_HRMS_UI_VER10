using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services
{
    public class JobPostingService: IJobPostingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JobPostingService(IServiceBase httpService)
        {
            _baseURL = "JobPosting";
            _httpService = httpService;
        }
        public async Task<JobPosting> GetJobPostingList(int TabIndex)
        {
            return await _httpService.GetAsync<JobPosting>($"{_baseURL}/GetJobPostingList?Tabindex={TabIndex}");
        }
        public async Task<JobPosting> GetJobPostingDetails(Guid JobPostingGI,string mode)
        {
            return await _httpService.GetAsync<JobPosting>($"{_baseURL}/GetJobPostingDetails?JobPostingGI={JobPostingGI}&Mode={mode}");
        }
        public async Task<ResponseEntity> JobPostingSave(JobPosting model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobPosting>($"{_baseURL}/JobPostingSave", model);
        }
        public async Task<ResponseEntity> JobPostingDelete(Guid VacancyManagementGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/JobPostingDelete?VacancyManagementGI={VacancyManagementGI}");
        }
    }
}
