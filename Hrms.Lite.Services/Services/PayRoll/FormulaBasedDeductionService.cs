using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class FormulaBasedDeductionService : IFormulaBasedDeductionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public FormulaBasedDeductionService(IServiceBase httpService)
        {
            _baseURL = "FormulaBasedDeduction";
            _httpService = httpService;
        }
        public async Task<FormulaBasedDeduction> GetFormulaBasedDeductionDetails(int DeductionCode)
        {
            return await _httpService.GetAsync<FormulaBasedDeduction>($"{_baseURL}/GetFormulaBasedDeductionDetails?DeductionCode={DeductionCode}");
        }
        public async Task<ResponseEntity> FormulaBasedDeductionSave(FormulaBasedDeduction model)
        {
            return await _httpService.PostAsync<ResponseEntity, FormulaBasedDeduction>($"{_baseURL}/FormulaBasedDeductionSave", model);
        }
    }
}
