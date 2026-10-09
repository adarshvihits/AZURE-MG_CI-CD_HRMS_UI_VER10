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

    public class CostCenterService : ICostCenterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public CostCenterService(IServiceBase httpService)
        {
            _baseURL = "CostCenter";
            _baseURL1 = "Common";

            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(CostCenter model)
        {
            return _httpService.PostAsync<ResponseEntity, CostCenter>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(CostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, CostCenter>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(CostCenter model)
        {
            return _httpService.PostAsync<ResponseEntity, CostCenter>($"{_baseURL}/Delete", model);
        }

        public async Task<CostCenter> GeCostCenterDetails(Guid CostCenterGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<CostCenter>($"{_baseURL}/CostCenterDetails?CostCenterGI={CostCenterGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<CostCenter> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<CostCenter>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
