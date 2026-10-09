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
    public class JobMasterService : IJobMasterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JobMasterService(IServiceBase httpService)
        {
            _baseURL = "JobMaster";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(JobMaster Input)
        {
            return _httpService.PostAsync<ResponseEntity, JobMaster>($"{_baseURL}/ApproveOrReject", Input);
        }

        public Task<ResponseEntity> Create(JobMaster Input)
        {
            return _httpService.PostAsync<ResponseEntity, JobMaster>($"{_baseURL}/Create", Input);
        }

        public async Task<JobMaster> GetList(int DepartmentCode, string Status)
        {
            return await _httpService.GetAsync<JobMaster>($"{_baseURL}/List?DepartmentCode={DepartmentCode}&Status={Status}");
        }

        public Task<JobMaster> GetJobMasterDetails(Guid JobGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<JobMaster>($"{_baseURL}/GetJobMasterDetails?JobGI={JobGI}&LogGI={LogGI}&Type={Type}");
        }
    }
}
