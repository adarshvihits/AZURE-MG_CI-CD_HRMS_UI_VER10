using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDepartmentCostCenterService
    {

        public Task<DepartmentCostCenter> GetList(string TabIndex);
        public Task<ResponseEntity> Create(DepartmentCostCenter input);
        public Task<DepartmentCostCenter> GetDepartmentCostCenterDetails(Guid LogGI, Guid DepartmentCostCenterGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(DepartmentCostCenter input);
        public Task<ResponseEntity> Delete(DepartmentCostCenter input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
