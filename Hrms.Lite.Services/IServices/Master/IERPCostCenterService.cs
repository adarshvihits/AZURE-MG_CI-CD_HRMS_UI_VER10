using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.Master
{
    public interface IERPCostCenterService
    {

        public Task<ERPCostCenter> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ERPCostCenter input);
        public Task<ERPCostCenter> GetERPCostCenterDetails(Guid LogGI, Guid ERPCostCenterGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ERPCostCenter input);
        public Task<ResponseEntity> Delete(ERPCostCenter input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
