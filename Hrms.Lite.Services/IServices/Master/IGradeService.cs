using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IGradeService
    {
        public Task<Grade> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Grade Input);
        public Task<Grade> GetGradeDetails(Guid GradeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Grade Input);
        public Task<ResponseEntity> ApproveOrReject(Grade model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
