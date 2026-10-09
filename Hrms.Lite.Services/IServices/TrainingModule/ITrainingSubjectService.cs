using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingSubjectService
    {
        public Task<TrainingSubject> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TrainingSubject input);
        public Task<TrainingSubject> GetSubjectDetails(Guid TrainingSubjectGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(TrainingSubject model);
        public Task<ResponseEntity> Delete(TrainingSubject model);
    }
}
