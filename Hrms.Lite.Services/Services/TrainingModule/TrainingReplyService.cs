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
   public class TrainingReplyService : ITrainingReplyService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public TrainingReplyService(IServiceBase httpService)
        {
            _baseURL = "TrainingReply";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingReply> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingReply>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(TrainingReply input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingReply>($"{_baseURL}/Create", input);
        }
        public Task<TrainingReply> GetReplyDetails(Guid ReplyGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingReply>($"{_baseURL}/ReplyDetails?ReplyGI={ReplyGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(TrainingReply model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingReply>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(TrainingReply model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingReply>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
