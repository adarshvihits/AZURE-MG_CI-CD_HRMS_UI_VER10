using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
    public class TRAININGCOMPLETIONService: ITRAININGCOMPLETIONService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TRAININGCOMPLETIONService(IServiceBase httpService)
        {
            _baseURL = "TrainingCompletion";
            _httpService = httpService;
        }
        public async Task<TrainingCompletion> GetList(int StatusCode, int FinancialYearCode)
        {
            return await _httpService.GetAsync<TrainingCompletion>($"{_baseURL}/GetList?StatusCode={StatusCode}&FinancialYearCode={FinancialYearCode}");
        }
        public async Task<TrainingCompletion> GetCostList(int ScheduleCode)
        {
            return await _httpService.GetAsync<TrainingCompletion>($"{_baseURL}/GetCostList?ScheduleCode={ScheduleCode}");
        }


        public Task<ResponseEntity> Create(TrainingCompletion input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingCompletion>($"{_baseURL}/Create", input);
        }

    }
}
