using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISalaryStructureService
    {
        public Task<SalaryStructureMaster> GetList(string TabIndex);
        public Task<SalaryStructureMaster> GetResultList(Guid SalaryStructureGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Save(SalaryStructureMaster model);
        public Task<ResponseEntity> Delete(Guid gi);
        public Task<ResponseEntity> ApproveOrReject(SalaryStructureMaster model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
