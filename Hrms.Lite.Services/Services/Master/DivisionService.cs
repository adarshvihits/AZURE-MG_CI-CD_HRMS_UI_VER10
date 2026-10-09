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
    public class DivisionService : IDivisionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DivisionService(IServiceBase httpService)
        {
            _baseURL = "Division";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Division model)
        {
            return _httpService.PostAsync<ResponseEntity, Division>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Division input)
        {
            return _httpService.PostAsync<ResponseEntity, Division>($"{_baseURL}/Create", input);
        }
            public async Task<Division> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Division>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<Division> GetDivisionDetails(Guid DivisionGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Division>($"{_baseURL}/GetDivisionDetails?DivisionGI={DivisionGI}&LogGI={LogGI}&Type={Type}");
        }

        public Task<ResponseEntity> Delete(Division input)
        {
            return _httpService.PostAsync<ResponseEntity, Division>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
