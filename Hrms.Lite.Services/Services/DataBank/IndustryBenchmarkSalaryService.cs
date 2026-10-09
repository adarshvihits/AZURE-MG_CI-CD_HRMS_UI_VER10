using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class IndustryBenchmarkSalaryService : IIndustryBenchmarkSalaryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public IndustryBenchmarkSalaryService(IServiceBase httpService)
        {
            _baseURL = "IndustryBenchmarkSalary";
            _httpService = httpService;
        }
        async Task<IndustryBenchmarkSalary> IIndustryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList(string Tab, Filter filter)
        {
            return await _httpService.PostAsync<IndustryBenchmarkSalary,Filter>($"{_baseURL}/GetIndustryBenchmarkSalaryList?Type={Tab}", filter);
        }


        async Task<IndustryBenchmarkSalary> IIndustryBenchmarkSalaryService.GetBenchmarkSalaryDetails()
        {
            return await _httpService.GetAsync<IndustryBenchmarkSalary>($"{_baseURL}/GetBenchmarkSalaryDetails");
        }
        async Task<ResponseEntity> IIndustryBenchmarkSalaryService.BenchmarkSalarySave(IndustryBenchmarkSalary model)
        {
            return await _httpService.PostAsync<ResponseEntity, IndustryBenchmarkSalary>($"{_baseURL}/BenchmarkSalarySave", model);
        }
        public async Task<DataSet> GetIndustryBenchmarkSalaryList_Excel(string Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetIndustryBenchmarkSalaryList_Excel?Type={Type}", filter);
        }
    }
}
