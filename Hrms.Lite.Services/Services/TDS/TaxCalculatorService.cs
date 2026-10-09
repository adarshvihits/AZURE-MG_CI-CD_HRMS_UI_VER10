using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS
{
    public class TaxCalculatorService:ITaxCalculatorService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TaxCalculatorService(IServiceBase httpService)
        {
            _baseURL = "TaxCalculator";
            _httpService = httpService;
        }
        public async Task<TaxCalculator> GetTaxComparisonDetails(Guid EmployeeGI, int AssessmentYear, int Parameter)
        {
            return await _httpService.GetAsync<TaxCalculator>($"{_baseURL}/GetTaxComparisonDetails?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}&Parameter={Parameter}");
        }
        public async Task<TaxCalculator> GetList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<TaxCalculator, Filter>($"{_baseURL}/GetEmpList?AssessmentYear={AssessmentYear}", filter);
        }
    }
}
