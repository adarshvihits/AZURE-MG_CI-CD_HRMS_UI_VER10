using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ERPCostCenterService : IERPCostCenterService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ERPCostCenterService(IServiceBase httpService)
        {
            _baseURL = "ERPCostCenter";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(ERPCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPCostCenter>($"{_baseURL}/Create", input);
        }

        public async Task<ERPCostCenter> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ERPCostCenter>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ERPCostCenter> GetERPCostCenterDetails(Guid LogGI, Guid ERPCostCenterGI, string Type)
        {
            return _httpService.GetAsync<ERPCostCenter>($"{_baseURL}/GetERPCostCenterDetails?LogGI={LogGI}&ERPCostCenterGI={ERPCostCenterGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(ERPCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPCostCenter>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(ERPCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPCostCenter>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
