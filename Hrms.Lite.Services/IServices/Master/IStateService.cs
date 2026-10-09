using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IStateService
    {
        public Task<State> GetList(string TabIndex);
        public Task<ResponseEntity> Create(State input);
        public Task<State> GetStateDetails(Guid StateGI, Guid  LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(State model);
        public Task<ResponseEntity> Delete(State model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
