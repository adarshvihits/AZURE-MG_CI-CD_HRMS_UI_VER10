using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IInterviewTypeService
    {
        public Task<InterviewType> GetList(string TabIndex);
        public Task<ResponseEntity> Save(InterviewType Input);
        public Task<InterviewType> GetInterviewTypeDetails(Guid InterviewTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(InterviewType model);
        public Task<ResponseEntity> Delete(InterviewType Input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
