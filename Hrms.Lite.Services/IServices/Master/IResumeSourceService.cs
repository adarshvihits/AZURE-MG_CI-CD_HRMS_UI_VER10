using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IResumeSourceService
    {
        public Task<ResponseEntity> Create(ResumeSources input);
        public Task<ResumeSources> GetList(string TabIndex);
        public Task<ResumeSources> GetDetails(Guid ResumeSourceGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(ResumeSources model);
        public Task<ResponseEntity> ApproveOrReject(ResumeSources model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
