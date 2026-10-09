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
    public class FunctionService: IFunctionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FunctionService(IServiceBase httpService)
        {
            _baseURL = "Function";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<Function> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Function>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(Function input)
        {
            return _httpService.PostAsync<ResponseEntity, Function>($"{_baseURL}/Create", input);
        }

        public async Task<Function> GetFunctionDetails(Guid FunctionGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Function>($"{_baseURL}/FunctionDetails?FunctionGI={FunctionGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(Function model)
        {
            return _httpService.PostAsync<ResponseEntity, Function>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(Function model)
        {
            return _httpService.PostAsync<ResponseEntity, Function>($"{_baseURL}/ApproveOrReject", model);
        }

        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
