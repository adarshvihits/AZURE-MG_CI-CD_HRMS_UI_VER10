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
    public class DepartmentCostCenterService : IDepartmentCostCenterService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DepartmentCostCenterService(IServiceBase httpService)
        {
            _baseURL = "DepartmentCostCenter";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(DepartmentCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, DepartmentCostCenter>($"{_baseURL}/Create", input);
        }

        public async Task<DepartmentCostCenter> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<DepartmentCostCenter>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<DepartmentCostCenter> GetDepartmentCostCenterDetails(Guid LogGI, Guid DepartmentCostCenterGI, string Type)
        {
            return _httpService.GetAsync<DepartmentCostCenter>($"{_baseURL}/GetDepartmentCostCenterDetails?LogGI={LogGI}&DepartmentCostCenterGI={DepartmentCostCenterGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(DepartmentCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, DepartmentCostCenter>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(DepartmentCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, DepartmentCostCenter>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
