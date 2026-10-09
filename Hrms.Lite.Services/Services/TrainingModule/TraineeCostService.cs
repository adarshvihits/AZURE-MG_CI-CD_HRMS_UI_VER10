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
  public  class TraineeCostService: ITraineeCostService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TraineeCostService(IServiceBase httpService)
        {
            _baseURL = "TraineeCost";
            _httpService = httpService;
        }
        public async Task<TraineeCost> GetScheduleList(int ScheduleCode, Filter filter)
        {
            return await _httpService.PostAsync<TraineeCost, Filter>($"{_baseURL}/GetScheduleList?ScheduleCode={ScheduleCode}", filter);
        }

        public async Task<TraineeCost> GetCostDetails(Guid empGI)
        {
            return await _httpService.GetAsync<TraineeCost>($"{_baseURL}/GetCostDetails?EmployeeGI={empGI}");
        }
        public async Task<TraineeCost> Batchupdation(int ScheduleCode)
        {
            return await _httpService.GetAsync<TraineeCost>($"{_baseURL}/Batchupdation?ScheduleCode={ScheduleCode}");
        }
        public async Task<ResponseEntity> Save(int ScheduleCode, TraineeCost model)
        {
            return await _httpService.PostAsync<ResponseEntity, TraineeCost>($"{_baseURL}/Save?ScheduleCode={ScheduleCode}", model);
        }

        public async Task<ResponseEntity> BatchSave(int ScheduleCode, TraineeCost model)
        {
            return await _httpService.PostAsync<ResponseEntity, TraineeCost>($"{_baseURL}/BatchSave?ScheduleCode={ScheduleCode}", model);
        }


    }
}
