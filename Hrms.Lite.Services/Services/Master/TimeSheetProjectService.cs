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
    public class TimeSheetProjectService: ITimeSheetProjectService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TimeSheetProjectService(IServiceBase httpService)
        {
            _baseURL = "TimeSheetProject";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<TimeSheetProject> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TimeSheetProject>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> Create(TimeSheetProject input)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetProject>($"{_baseURL}/Create", input);
        }
        public async Task<TimeSheetProject> GetProjectDetails(Guid ProjectGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<TimeSheetProject>($"{_baseURL}/ProjectDetails?ProjectGI={ProjectGI}&&LogGI={LogGI}&&Type={Type}");
        }
        public async Task<ResponseEntity> ApproveOrReject(TimeSheetProject model)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetProject>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TimeSheetProject model)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetProject>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
