using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class SalaryReleaseWorkFlowService: ISalaryReleaseWorkFlowService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryReleaseWorkFlowService(IServiceBase httpService)
        {
            _baseURL = "SalaryReleaseWorkFlow";
            _httpService = httpService;
        }
        public async Task<SalaryReleaseWorkFlow> GetList(int Month, int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReleaseWorkFlow, Filter>($"{_baseURL}/GetList?Month={Month}&TabIndex={TabIndex}", filter);
        }
        public Task<ResponseEntity> Save(SalaryReleaseWorkFlow input)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryReleaseWorkFlow>($"{_baseURL}/Save", input);
        }
        public async Task<DataSet> GetList_Excel(int Month, int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Month={Month}&TabIndex={TabIndex}", filter);
        }
    }
}
