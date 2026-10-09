using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class TrainingReplyGroupService : ITrainingReplyGroupServices
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingReplyGroupService(IServiceBase httpService)
        {
            _baseURL = "TrainingReplyGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<TrainingReplyGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingReplyGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(TrainingReplyGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingReplyGroup>($"{_baseURL}/Create", input);
        }
        public Task<TrainingReplyGroup> GetReplygroupDetails(Guid ReplyGroupGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingReplyGroup>($"{_baseURL}/ReplygroupDetails?ReplyGroupGI={ReplyGroupGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(TrainingReplyGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingReplyGroup>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(TrainingReplyGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingReplyGroup>($"{_baseURL}/ApproveOrReject", model);
        }
    }
}
