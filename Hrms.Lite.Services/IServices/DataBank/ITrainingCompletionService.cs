using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ITrainingCompletionService
    {
        public Task<InductionTraining> GetList(Filter filter);
        public Task<ResponseEntity> ApproveCompletion(InductionTraining model);
        public Task<InductionTraining> GetInductionTrainingDetails(int HeadCode, int EmployeeCode);
    }
}
