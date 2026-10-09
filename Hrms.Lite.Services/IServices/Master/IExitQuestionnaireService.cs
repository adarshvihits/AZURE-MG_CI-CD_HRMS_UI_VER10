using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IExitQuestionnaireService
    {
        public Task<ExitInterviewQuestionnaire> GetQuestionnaireList(string TabIndex);
        public Task<ExitInterviewQuestionnaire> GetDetails(Guid QuestionnaireGI,Guid LogGI,string Type);
        public Task<ResponseEntity> Create(ExitInterviewQuestionnaire input);
        public Task<ResponseEntity> ResultSave(List<PossibleResultsClassifications> input);
        public Task<ResponseEntity> Delete(Guid QuestionnaireGI);
        public Task<ExitInterviewQuestionnaire> GetResultList();
        public Task<ResponseEntity> ApproveOrReject(ExitInterviewQuestionnaire input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
