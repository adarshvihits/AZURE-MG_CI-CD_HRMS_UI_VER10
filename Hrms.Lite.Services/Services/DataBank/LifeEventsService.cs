using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class LifeEventsService : ILifeEventsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LifeEventsService(IServiceBase httpService)
        {
            _baseURL = "LifeEvents";
            _httpService = httpService;
        }
        async Task<LifeEvents> ILifeEventsService.GetCelebrities(int CelebrationCombo1, string Classification)
        {
            return await _httpService.GetAsync<LifeEvents>($"{_baseURL}/GetCelebrities?CelebrationCombo1={CelebrationCombo1}&Classification={Classification}");
        }
        async Task<ResponseEntity> ILifeEventsService.Save(LifeEvents model)
        {
            return await _httpService.PostAsync<ResponseEntity,LifeEvents>($"{_baseURL}/Create",model);
        }
        async Task<LifeEvents> ILifeEventsService.GetEventList(string Mode)
        {
            return await _httpService.GetAsync<LifeEvents>($"{_baseURL}/GetEventList?Mode={Mode}");
        }
        async Task<LifeEvents> ILifeEventsService.LifeEventDetails(Guid EventGI)
        {
            return await _httpService.GetAsync<LifeEvents>($"{_baseURL}/LifeEventDetails?EventGI={EventGI}");
        }
        async Task<ResponseEntity> ILifeEventsService.Delete(Guid EventGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?EventGI={EventGI}");
        }
    }
}
