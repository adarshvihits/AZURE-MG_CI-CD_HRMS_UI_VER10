using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IIncentiveCostCenterService
    {
        public Task<IncentiveCostCenter> GetList(string TabIndex);
        public Task<ResponseEntity> Create(IncentiveCostCenter input);
        public Task<IncentiveCostCenter> GetIncentiveCostCenterDetails(Guid IncentiveCostCenterGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(IncentiveCostCenter model);
        public Task<ResponseEntity> ApproveOrReject(IncentiveCostCenter model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
