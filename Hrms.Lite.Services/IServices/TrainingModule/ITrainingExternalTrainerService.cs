using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingExternalTrainerService
    {
        public Task<TrainingExternalTrainer> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingExternalTrainer input);
        public Task<TrainingExternalTrainer> GetSubjectDetails(Guid TrainingExternalTrainerGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(TrainingExternalTrainer model);
        public Task<ResponseEntity> Delete(TrainingExternalTrainer model);
    }
}
