using Hrms.Lite.Shared.TrainingModule;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrainingReplyService
    {
        public Task<TrainingReply> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingReply input);
        public Task<TrainingReply> GetReplyDetails(Guid ReplyGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(TrainingReply model);
        public Task<ResponseEntity> ApproveOrReject(TrainingReply model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
