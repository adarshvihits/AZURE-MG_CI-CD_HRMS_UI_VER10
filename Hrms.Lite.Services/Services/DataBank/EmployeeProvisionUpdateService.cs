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
    public class EmployeeProvisionUpdateService : IEmployeeProvisionUpdateService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeProvisionUpdateService(IServiceBase httpService)
        {
            _baseURL = "EmployeeProvisionUpdate";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<EmployeeProvisionUpdate> GetEmployeeProvisionUpdateDetails(Guid EmployeeGI, int AllowanceCode)
        {
            return await _httpService.GetAsync<EmployeeProvisionUpdate>($"{_baseURL}/GetEmployeeProvisionUpdateDetails?EmployeeGI={EmployeeGI}&&AllowanceCode={AllowanceCode}");
        }

        async Task<EmployeeProvisionUpdate> IEmployeeProvisionUpdateService.GetList(int AllowanceCode, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeProvisionUpdate, Filter>($"{_baseURL}/GetList?AllowanceCode={AllowanceCode}", filterdata);
        }



        public Task<ResponseEntity> Create(EmployeeProvisionUpdate input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeProvisionUpdate>($"{_baseURL}/Create", input);
        }


        public Task<ResponseEntity> Delete(EmployeeProvisionUpdate model)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeProvisionUpdate>($"{_baseURL}/Delete", model);
        }

        public async Task<DataSet> GetList_Excel(int AllowanceCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?AllowanceCode={AllowanceCode}", Filter);
        }
    }
}

