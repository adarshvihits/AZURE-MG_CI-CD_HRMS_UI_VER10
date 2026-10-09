using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IAssetAllocationService
    {
        public Task<IList<AssetAllocationTabs>> GetAssetAllocationTabs();
        public Task<AssetAllocation> GetAssetAllocationList(int Tabindex,int SearchId, int AssetCategoryCode,Filter Filter);        
        public Task<AssetAllocation> GetEmployeeCard(int EmpCode);
        public Task<AssetAllocation> GetAssetAllocationDetails(Guid AssetAllocationGI);
        public Task<AssetAllocation> GetEmpAssetReqDetails(Guid EmployeeGI, int TrnCode);
        public Task<AssetAllocation> GetIssueDetails(Guid AssetAllocationGI);
        public Task<AssetAllocation> GetAssetDetails(int AssetCode);
        public Task<ResponseEntity> AssetAllocationDetailsSave(AssetAllocation model);
        public Task<ResponseEntity> AssetAllocationDelete(Guid AssetAllocationGI);
        public Task<IList<AssetLog>> GetAssetLogList(Guid EmployeeGI);
        public Task<AssetAllocation> GetAssetLogDetails(Guid AssetAllocationGI);
        public Task<ResponseEntity> AssetReturn(AssetAllocation model);
        public Task<ResponseEntity> NewRequest(AssetAllocation model);
        public Task<ResponseEntity> AssetAllocationApprove(AssetAllocation model);
        public Task<ResponseEntity> AssetAllocationSave(AssetAllocation model);
        public Task<ResponseEntity> AssetIssue(AssetAllocation model);
        public Task<ResponseEntity>Approve(AssetAllocation model);
        public Task<ResponseEntity> Reject(AssetAllocation model);

    }
}
