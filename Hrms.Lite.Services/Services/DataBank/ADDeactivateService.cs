
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
    public class ADDeactivateService : IADDeactivateService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ADDeactivateService(IServiceBase httpService)
        {
            _baseURL = "ADDeactivate";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<ADDeactivate> GetADDeactivateDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<ADDeactivate>($"{_baseURL}/GetADDeactivateDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<ADDeactivate> IADDeactivateService.GetList(int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<ADDeactivate, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filterdata);
        }



        public Task<ResponseEntity> Create(ADDeactivate input)
        {
            return _httpService.PostAsync<ResponseEntity, ADDeactivate>($"{_baseURL}/Create", input);
        }

        public async Task<DataSet> GetList_Excel(int Tabindex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}", Filter);
        }
    }
}
