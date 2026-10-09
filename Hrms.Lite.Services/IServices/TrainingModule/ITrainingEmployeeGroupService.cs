using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrainingEmployeeGroupService
    {
        public Task<TrainingEmployeeGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingEmployeeGroup input);
        public Task<TrainingEmployeeGroup> GEtCreateTraining();
        public Task<TrainingEmployeeGroup> GetDetails(Guid TrainingGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(TrainingEmployeeGroup model);
        public Task<ResponseEntity> ApproveOrReject(TrainingEmployeeGroup model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
