using Hrms.Lite.Shared.TrainingModule;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingReplyGroupServices
    {
        public Task<TrainingReplyGroup> GetList(string TabIndex);
         public Task<ResponseEntity> Create(TrainingReplyGroup input);
        public Task<TrainingReplyGroup> GetReplygroupDetails(Guid ReplyGroupGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(TrainingReplyGroup model);
        public Task<ResponseEntity> ApproveOrReject(TrainingReplyGroup model);
    }
}
