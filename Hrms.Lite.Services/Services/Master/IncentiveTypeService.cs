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
    public class IncentiveTypeService: IIncentiveTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public IncentiveTypeService(IServiceBase httpService)
        {
            _baseURL = "IncentiveType";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<IncentiveType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<IncentiveType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(IncentiveType input)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveType>($"{_baseURL}/Create", input);
        }

        public async Task<IncentiveType> GetIncentiveTypeDetails(Guid IncentiveTypeGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<IncentiveType>($"{_baseURL}/IncentiveTypeDetails?IncentiveTypeGI={IncentiveTypeGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(IncentiveType model)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveType>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(IncentiveType model)
        {
            return _httpService.PostAsync<ResponseEntity, IncentiveType>($"{_baseURL}/ApproveOrReject", model);
        }


        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
