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
    public class EmployeeVsRenewalDateService : IEmployeeVsRenewalDateService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeVsRenewalDateService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsRenewalDate";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<EmployeeVsRenewalDate> GetEmployeeVsRenewalDateDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<EmployeeVsRenewalDate>($"{_baseURL}/GetEmployeeVsRenewalDateDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<EmployeeVsRenewalDate> IEmployeeVsRenewalDateService.GetList(int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeVsRenewalDate, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filterdata);
        }



        public Task<ResponseEntity> Create(EmployeeVsRenewalDate input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsRenewalDate>($"{_baseURL}/Create", input);
        }


        public Task<ResponseEntity> Delete(EmployeeVsRenewalDate model)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsRenewalDate>($"{_baseURL}/Delete", model);
        }

  
    }
}
