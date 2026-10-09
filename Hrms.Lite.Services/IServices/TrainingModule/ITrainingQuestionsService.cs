using Hrms.Lite.Shared.TrainingModule;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingQuestionsService
    {
        public Task<TrainingQuestions> GetList(string TabIndex,int TrainingCode);
        public Task<ResponseEntity> Create(TrainingQuestions input);
        public Task<TrainingQuestions> GetTrainingQuestionsDetails(Guid TrainingQuestionGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(TrainingQuestions model);
        public Task<ResponseEntity> ApproveOrReject(TrainingQuestions model);
    }
}
