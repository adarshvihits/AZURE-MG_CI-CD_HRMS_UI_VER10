using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsNpsService : IEmployeeVsNpsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeVsNpsService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsNps";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<EmployeeVsNps> GetEmployeeVsNpsDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<EmployeeVsNps>($"{_baseURL}/GetEmployeeVsNpsDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<EmployeeVsNps> IEmployeeVsNpsService.GetList(int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeVsNps, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filterdata);
        }



        public Task<ResponseEntity> Create(EmployeeVsNps input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsNps>($"{_baseURL}/Create", input);
        }


        public Task<ResponseEntity> Delete(EmployeeVsNps model)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsNps>($"{_baseURL}/Delete", model);
        }

        public async Task<DataSet> GetList_Excel(int Tabindex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}", Filter);
        }
    }
}
