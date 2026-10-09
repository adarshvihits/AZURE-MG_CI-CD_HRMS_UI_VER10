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
   public class TrnParticipantService: ITrnParticipantService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrnParticipantService(IServiceBase httpService)
        {
            _baseURL = "TrnParticipantSelection";
            _httpService = httpService;
        }
        public async Task<TrnParticipantSelection> GetScheduleList(int ScheduleCode, Filter filter)
        {
            return await _httpService.PostAsync<TrnParticipantSelection, Filter>($"{_baseURL}/GetScheduleList?ScheduleCode={ScheduleCode}", filter);
        }
        public async Task<TrnParticipantSelection> GetFilteredList(int ScheduleCode, Filter filter)
        {
            return await _httpService.PostAsync<TrnParticipantSelection, Filter>($"{_baseURL}/GetFilteredList?ScheduleCode={ScheduleCode}", filter);
        }
        public async Task<ResponseEntity> DeleteAll(int ScheduleCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/DeleteAll?ScheduleCode={ScheduleCode}");
        }
        public async Task<ResponseEntity> Save(int ScheduleCode, TrnParticipantSelection model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrnParticipantSelection>($"{_baseURL}/Save?ScheduleCode={ScheduleCode}", model);
        }
        public async Task<ResponseEntity> DeleteParticipant(Guid EmployeeGI, int ScheduleCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/DeleteParticipant?EmployeeGI={EmployeeGI}&ScheduleCode={ScheduleCode}");
        }
    }
}
