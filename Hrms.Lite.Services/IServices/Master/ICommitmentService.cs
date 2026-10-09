using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICommitmentService
    {

        public Task<ResponseEntity> Create(Commitment input);
        public Task<Commitment> GetList(string TabIndex);
        public Task<Commitment> GetCommitmentDetails(Guid CommitmentGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Commitment model);
        public Task<ResponseEntity> ApproveOrReject(Commitment model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);



    }
}
