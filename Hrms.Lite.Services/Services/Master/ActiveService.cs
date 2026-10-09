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
    public class ActivityService : IActivityService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ActivityService(IServiceBase httpService)
        {
            _baseURL = "Activity";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public Task<ResponseEntity> ApproveOrReject(Activity model)
        {
            return _httpService.PostAsync<ResponseEntity, Activity>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Activity input)
        {
            return _httpService.PostAsync<ResponseEntity, Activity>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Activity model)
        {
            return _httpService.PostAsync<ResponseEntity, Activity>($"{_baseURL}/Delete", model);
        }

        public async Task<Activity> GetActivityDetails(Guid ActivityGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Activity>($"{_baseURL}/ActivityDetails?ActivityGI={ActivityGI}&&LogGI={LogGI}&&Type={Type}");
        }

        


        public async Task<Activity> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Activity>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

       
    }
}
