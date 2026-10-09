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
   public class ParticipantEntryService: IParticipantEntryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ParticipantEntryService(IServiceBase httpService)
        {
            _baseURL = "ParticipantEntry";
            _httpService = httpService;
        }
        public async Task<ParticipantEntry> GetScheduleList(int ScheduleCode, Filter filter,int radiomode)
        {
            return await _httpService.PostAsync<ParticipantEntry, Filter>($"{_baseURL}/GetScheduleList?ScheduleCode={ScheduleCode}&radiomode={radiomode}", filter);
        }
     
        public async Task<ResponseEntity> Save(int ScheduleCode, ParticipantEntry model,int radiomode)
        {
            return await _httpService.PostAsync<ResponseEntity, ParticipantEntry>($"{_baseURL}/Save?ScheduleCode={ScheduleCode}&radiomode={radiomode}", model);
        }
 
    }
}
