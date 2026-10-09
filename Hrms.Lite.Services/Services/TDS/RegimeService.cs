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
   public class RegimeService: IRegimeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RegimeService(IServiceBase httpService)
        {
            _baseURL = "Regime";
            _httpService = httpService;
        }
        public async Task<List<RegimeSelectionEmployeeList>> GetRegimeEmployeeList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<List<RegimeSelectionEmployeeList>, Filter>($"{_baseURL}/GetRegimeEmployeeList?AssessmentYear={AssessmentYear}", filter);
        }
        public async Task<ResponseEntity> SaveRegimeSelection(RegimeSelectionEmployeeList model, int AssessmentYear)
        {
            return await _httpService.PostAsync<ResponseEntity, RegimeSelectionEmployeeList>($"{_baseURL}/SaveRegimeSelection?AssessmentYear={AssessmentYear}", model);
        }
        public async Task<ResponseEntity> BatchUpdateRegimeSelection(RegimeBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, RegimeBatchUpdate>($"{_baseURL}/BatchUpdateRegimeSelection", model);
        }
    }
}
