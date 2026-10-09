using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class InstantMessagingService : IInstantMessagingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public InstantMessagingService(IServiceBase httpService)
        {
            _baseURL = "InstantMessaging";
            _httpService = httpService;
        }
        public async Task<InstantMessaging> GetMessageList()
        {
            return await _httpService.GetAsync<InstantMessaging>($"{_baseURL}/GetMessageList");
        }
        public Task<InstantMessaging> GetMessageDetails(int MessageCode)
        {
            return _httpService.GetAsync<InstantMessaging>($"{_baseURL}/GetMessageDetails?MessageCode={MessageCode}");
        }
        public async Task<ResponseEntity> Message_Save(InstantMessaging model)
        {
            return await _httpService.PostAsync<ResponseEntity, InstantMessaging>($"{_baseURL}/Message_Save", model);
        }
        public async Task<InstantMessaging> GetEmployeesList(Filter filter,int MessageCode)
        {
            return await _httpService.PostAsync<InstantMessaging, Filter>($"{_baseURL}/GetEmployeeList?MessageCode={MessageCode}", filter);
        }
        public async Task<ResponseEntity> Employee_Save(InstantMessaging model)
        {
            return await _httpService.PostAsync<ResponseEntity, InstantMessaging>($"{_baseURL}/Employee_Save", model);
        }
        public Task<ResponseEntity> Message_Send(int MessageCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/Message_Send?MessageCode={MessageCode}");
        }
    }
}
