using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ITrainingMasterService
    {
        public Task<TrainingMaster> GetTrainingList(string Tabindex);
        public Task<TrainingMaster> GetTrainingMasterDetails(Guid TrainingGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(TrainingMaster model);
        public Task<ResponseEntity> ApproveOrReject(TrainingMaster model);
        public Task<ResponseEntity> Delete(Guid TrainingGI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
