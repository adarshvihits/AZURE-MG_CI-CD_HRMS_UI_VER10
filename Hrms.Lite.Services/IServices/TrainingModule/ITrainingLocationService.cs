using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingLocationService
    {
        public Task<TrainingLocation> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingLocation input);
        public Task<TrainingLocation> GetDetails(Guid TrainingExternalLocationGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(TrainingLocation model);
        public Task<ResponseEntity> Delete(TrainingLocation model);
    }
}
