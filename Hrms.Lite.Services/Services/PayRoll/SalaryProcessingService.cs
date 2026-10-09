using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class SalaryProcessingService : ISalaryProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryProcessingService(IServiceBase httpService)
        {
            _baseURL = "SalaryProcessing";
            _httpService = httpService;
        }   

      public async Task<SalaryProcessing> SalaryProcessingValidations(int SalaryPeriodCode, int PayrollGroup)
        {
            return await _httpService.GetAsync<SalaryProcessing>($"{_baseURL}/SalaryProcessingValidations?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroup={PayrollGroup}");
        }
        public async Task<ResponseEntity<SalaryProcessing>> MonthlySalaryProcessingValidation(SalaryProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity<SalaryProcessing>, SalaryProcessing>($"{_baseURL}/MonthlySalaryProcessingValidation", model);
         
        }
        public async Task<ResponseEntity<SalaryProcessing>> SalaryProcessing(SalaryProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity<SalaryProcessing>, SalaryProcessing>($"{_baseURL}/SalaryProcessing", model);
        }


        public async Task<SalaryProcessing> SalaryProcessingValidationsPopUp(int SalaryPeriodCode, int PayrollGroup, string TabName, string Description)
        {
            return await _httpService.GetAsync<SalaryProcessing>($"{_baseURL}/SalaryProcessingValidationsPopUp?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroup={PayrollGroup}&TabName={TabName}&Description={Description}");
        }

        
    }
}
