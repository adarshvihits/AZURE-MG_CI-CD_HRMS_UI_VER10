using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IWageGradeService
    {
        public Task<WageGradeMaster> GetWageGradeList(string TabIndex);
        public Task<ResponseEntity> Create(WageGradeMaster Input);
        public Task<WageGradeMaster> GetWageGradeDetails(Guid WageGradeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Guid GI);
        public Task<ResponseEntity> ApproveOrReject(WageGradeMaster model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
