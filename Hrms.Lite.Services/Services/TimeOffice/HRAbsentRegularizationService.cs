using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class HRAbsentRegularizationService : IHRAbsentRegularizationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public HRAbsentRegularizationService(IServiceBase httpService)
        {
            _baseURL = "HRAbsentRegularization";
            _httpService = httpService;
        }
        async Task<HRAbsentRegularization> IHRAbsentRegularizationService.EmployeeWiseABDetails(int EmpCode, int Pgroup, int Month, int Leave)
        {
            return await _httpService.GetAsync<HRAbsentRegularization>($"{_baseURL}/EmployeeWiseABDetails?EmpCode={EmpCode}&Pgroup=" +
                $"{Pgroup}&Month={Month}&Leave={Leave}");

        }

       public async Task<ResponseEntity> EmployeeWiseRegularize(HRAbsentRegularization model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAbsentRegularization>($"{_baseURL}/EmployeeWiseRegularize", model);

        }

        async Task<HRAbsentRegularization> IHRAbsentRegularizationService.HRAbsentRegularizationList(Filter Filter, int Pgroup, int Month, int Leave)
        {
            return await _httpService.PostAsync<HRAbsentRegularization, Filter>($"{_baseURL}/HRAbsentRegularizationList?Pgroup=" +
                $"{Pgroup}&Month={Month}&Leave={Leave}", Filter);
        }

      public  async Task<DataSet> List_Excel(Filter Filter, int Pgroup, int Month, int Leave)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/List_Excel?Pgroup=" +
                $"{Pgroup}&Month={Month}&Leave={Leave}", Filter);
        }

        public async Task<ResponseEntity> Regularize(HRAbsentRegularization model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAbsentRegularization>($"{_baseURL}/Regularize", model);

        }
    }
}
