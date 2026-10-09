using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class IncentiveCostCenterService: IIncentiveCostCenterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public IncentiveCostCenterService(IServiceBase httpService)
        {
            _baseURL = "IncentiveCostCenter";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<IncentiveCostCenter> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<IncentiveCostCenter>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(IncentiveCostCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveCostCenter>($"{_baseURL}/Create", input);
        }

        public async Task<IncentiveCostCenter> GetIncentiveCostCenterDetails(Guid IncentiveCostCenterGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<IncentiveCostCenter>($"{_baseURL}/IncentiveCostCenterDetails?IncentiveCostCenterGI={IncentiveCostCenterGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(IncentiveCostCenter model)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveCostCenter>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(IncentiveCostCenter model)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveCostCenter>($"{_baseURL}/ApproveOrReject", model);
        }

        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }

    }
}
