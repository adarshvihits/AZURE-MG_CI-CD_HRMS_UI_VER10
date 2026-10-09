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
   
    public class WorkLocationService : IWorkLocationService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public WorkLocationService(IServiceBase httpService)
        {
            _baseURL = "WorkLocation";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<WorkLocation> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<WorkLocation>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(WorkLocation input)
        {
            return _httpService.PostAsync<ResponseEntity, WorkLocation>($"{_baseURL}/Create", input);
        }
        public Task<WorkLocation> GetWorkLocationDetails(Guid WorkLocationGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<WorkLocation>($"{_baseURL}/GetWorkLocationDetails?WorkLocationGI={WorkLocationGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(WorkLocation model)
        {
            return _httpService.PostAsync<ResponseEntity, WorkLocation>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(WorkLocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, WorkLocation>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
