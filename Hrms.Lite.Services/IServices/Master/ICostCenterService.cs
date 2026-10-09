using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface ICostCenterService
    {
        public Task<CostCenter> GetList(string TabIndex);
        public Task<ResponseEntity> Create(CostCenter input);
        public Task<CostCenter> GeCostCenterDetails(Guid CostCenterGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(CostCenter model);
        public Task<ResponseEntity> Delete(CostCenter model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
