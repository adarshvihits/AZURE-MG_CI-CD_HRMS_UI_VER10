using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
  public  interface ITrainingVsSubjectService
    {
        Task<TrainingVsSubject> GetList(int TrainingCode);
        public Task<ResponseEntity> SubjectSave(TrainingVsSubject model);
    }
}
