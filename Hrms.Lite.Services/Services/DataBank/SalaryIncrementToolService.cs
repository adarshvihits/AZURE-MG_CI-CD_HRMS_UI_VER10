using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class SalaryIncrementToolService : ISalaryIncrementToolService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryIncrementToolService(IServiceBase httpService)
        {
            _baseURL = "SalaryIncrementTool";
            _httpService = httpService;
        }

        public async Task<SalaryIncrementTool> GetSettingsList()
        {
            return await _httpService.GetAsync<SalaryIncrementTool>($"{_baseURL}/GetSettingsList");
        }
        public async Task<SalaryIncrementTool> GetAllowanceList()
        {
            return await _httpService.GetAsync<SalaryIncrementTool>($"{_baseURL}/GetAllowanceList");
        }
        public async Task<SalaryIncrementTool> GetClassificationList()
        {
            return await _httpService.GetAsync<SalaryIncrementTool>($"{_baseURL}/GetClassificationList");
        }
        public async Task<SalaryIncrementTool> GetFixedList()
        {
            return await _httpService.GetAsync<SalaryIncrementTool>($"{_baseURL}/GetFixedList");
        }
        public async Task<SalaryIncrementTool> GetFixedSettingsList()
        {
            return await _httpService.GetAsync<SalaryIncrementTool>($"{_baseURL}/GetFixedSettingsList");
        }
        public Task<ResponseEntity> Create(SalaryIncrementTool model)
        {

            return _httpService.PostAsync<ResponseEntity, SalaryIncrementTool>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity<List<SalaryIncrementTool>>> ImportFromExcel(SalaryIncrementTool model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<SalaryIncrementTool>>, SalaryIncrementTool>($"{_baseURL}/ImportFromExcel", model);

        }
        public async Task<ResponseEntity> PrepareDoccument(SalaryIncrementTool model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryIncrementTool>($"{_baseURL}/PrepareDoccument", model);

        }
        public async Task<ResponseEntity> ProcessExcelClick(SalaryIncrementTool model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryIncrementTool>($"{_baseURL}/ProcessExcelClick", model);

        }
    }
}
