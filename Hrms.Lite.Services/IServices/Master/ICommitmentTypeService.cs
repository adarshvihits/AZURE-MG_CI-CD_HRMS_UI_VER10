using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICommitmentTypeService
    {

        public Task<ResponseEntity> Create(CommitmentType input);
        public Task<CommitmentType> GetList(string TabIndex);
        public Task<CommitmentType> GetCommitmentTypeDetails(Guid CommitmentTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(CommitmentType model);
        public Task<ResponseEntity> ApproveOrReject(CommitmentType model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);



    }
}
