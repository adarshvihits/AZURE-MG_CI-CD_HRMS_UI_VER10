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
    public class EmployeeVsInsuranceDetailsService : IEmployeeVsInsuranceDetailsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeVsInsuranceDetailsService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsInsuranceDetails";
            _httpService = httpService;
        }
        public async Task<EmployeeVsInsuranceDetails> GetEmployeeList(int Policy, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsInsuranceDetails,Filter>($"{_baseURL}/GetEmployeeList?Policy={Policy}",filter);
        }
        public async Task<EmployeeVsInsuranceDetails> GetEmployeeInsuranceDetails(int Policy)
        {
            return await _httpService.GetAsync<EmployeeVsInsuranceDetails>($"{_baseURL}/GetEmployeeInsuranceDetails?Policy={Policy}");
        }
        public async Task<EmployeeVsInsuranceDetails> GetEmployeeVsInsuranceDetails(Guid EmployeeGUID,int PolicyCode)
        {
            return await _httpService.GetAsync<EmployeeVsInsuranceDetails>($"{_baseURL}/GetEmployeeVsInsuranceDetails?EmployeeGUID={EmployeeGUID}&Policy={PolicyCode}");
        }
        public async Task<DataSet> GetEmployeeVsInsuranceDetails_Excel(int Policy, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeVsInsuranceDetails_Excel?Policy={Policy}", filter);
        }
        public async Task<ResponseEntity> Create(EmployeeVsInsuranceDetails input)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsInsuranceDetails>($"{_baseURL}/Create", input);
        }
        public async Task<ResponseEntity> Update(EmployeeVsInsuranceDetails input)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsInsuranceDetails>($"{_baseURL}/Update", input);
        }
        public async Task<ResponseEntity<List<EmployeeVsInsuranceDetails>>> ImportFromExcel(EmployeeVsInsuranceDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<EmployeeVsInsuranceDetails>>, EmployeeVsInsuranceDetails>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
