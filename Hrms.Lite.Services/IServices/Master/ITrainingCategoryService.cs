using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ITrainingCategoryService
    {
        public Task<TrainingCategory> GetList(string TabIndex);
        public Task<TrainingCategory> GetTrainingCategoryDetails(Guid TrainingCategoryGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(TrainingCategory input);
        public Task<ResponseEntity> ApproveOrReject(TrainingCategory model);
        public Task<ResponseEntity> Delete(TrainingCategory model);


    }
}
