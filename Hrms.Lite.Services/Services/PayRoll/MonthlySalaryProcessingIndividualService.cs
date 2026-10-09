using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class MonthlySalaryProcessingIndividualService : IMonthlySalaryProcessingIndividualService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlySalaryProcessingIndividualService(IServiceBase httpService)
        {
            _baseURL = "SalaryProcessing";
            _httpService = httpService;
        }
        public async Task<SalaryProcessing> Individual_SalaryProcessingValidations(int SalaryPeriodCode, int EmployeeCode)
        {
            return await _httpService.GetAsync<SalaryProcessing>($"{_baseURL}/Individual_SalaryProcessingValidations?SalaryPeriodCode={SalaryPeriodCode}&EmployeeCode={EmployeeCode}");
        }

        public async Task<ResponseEntity<SalaryProcessing>> Individual_SalaryProcessing(SalaryProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity<SalaryProcessing>, SalaryProcessing>($"{_baseURL}/Individual_SalaryProcessing", model);
        }
        public async Task<SalaryIntegrityReport> Individual_SalaryProcessing_Informations(int SelectedMonth, int EmployeeCode)
        {
            return await _httpService.GetAsync<SalaryIntegrityReport>($"{_baseURL}/Individual_SalaryProcessing_Informations?SalaryPeriodCode={SelectedMonth}&EmployeeCode={EmployeeCode}");
        }
    }
}
