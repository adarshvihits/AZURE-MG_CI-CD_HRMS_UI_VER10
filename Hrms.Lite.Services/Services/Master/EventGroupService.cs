using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class EventGroupService: IEventGroupService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EventGroupService(IServiceBase httpService)
        {
            _baseURL = "EventGroup";
            _httpService = httpService;
        }
        public async Task<EventGroup> GetList(int tabIndex)
        {
            return await _httpService.GetAsync<EventGroup>($"{_baseURL}/List?tabIndex={tabIndex}");
        }
        public async Task<EventGroup> GetCombinationDetails(int id)
        {
            return await _httpService.GetAsync<EventGroup>($"{_baseURL}/CombinationDetails?Typecode={id}");
        }
        public async Task<EventGroup> EmployeeList(string Mode,int EventGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<EventGroup, Filter>($"{_baseURL}/EmployeeList?Mode={Mode}&EventGroupCode={EventGroupCode}", filter);
        }
        public async Task<ResponseEntity> Save(EventGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, EventGroup>($"{_baseURL}/Save", model);
        }
        public async Task<EventGroup> GetEventGroupDetails(int Code)
        {
            return await _httpService.GetAsync<EventGroup>($"{_baseURL}/GetEventGroupDetails?Code={Code}");
        }
        public async Task<ResponseEntity> Delete(int Code)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?Code={Code}");
        }
    }
}
