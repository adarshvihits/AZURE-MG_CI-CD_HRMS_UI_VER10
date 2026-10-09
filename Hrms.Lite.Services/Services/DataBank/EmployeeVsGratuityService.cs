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
    public class EmployeeVsGratuityService : IEmployeeVsGratuityService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeVsGratuityService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsGratuity";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<EmployeeVsGratuity> GetEmployeeVsGratuityDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<EmployeeVsGratuity>($"{_baseURL}/GetEmployeeVsGratuityDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<EmployeeVsGratuity> IEmployeeVsGratuityService.GetList(int Tabindex, Filter filterdata, string FromDate, string ToDate)
        {
            return await _httpService.PostAsync<EmployeeVsGratuity, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}&&FromDate={FromDate}&&ToDate={ToDate}", filterdata);
        }

       

        public Task<ResponseEntity> Create(EmployeeVsGratuity input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsGratuity>($"{_baseURL}/Create", input);
        }
      
        public async Task<DataSet> GetList_Excel(int Tabindex, Filter Filter, string FromDate, string ToDate)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}&&FromDate={FromDate}&&ToDate={ToDate}", Filter);
        }
    }
}

