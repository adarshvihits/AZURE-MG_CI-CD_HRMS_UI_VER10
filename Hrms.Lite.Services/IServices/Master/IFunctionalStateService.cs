using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.Master
{
    public interface IFunctionalStateService
    {

        public Task<FunctionalState> GetList(string TabIndex);
        public Task<ResponseEntity> Create(FunctionalState input);
        public Task<FunctionalState> GetFunctionalStateDetails(Guid LogGI, Guid FunctionalStateGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(FunctionalState input);
        public Task<ResponseEntity> Delete(FunctionalState input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
