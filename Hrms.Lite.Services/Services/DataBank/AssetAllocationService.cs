using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class AssetAllocationService : IAssetAllocationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AssetAllocationService(IServiceBase httpService)
        {
            _baseURL = "AssetAllocation";
            _httpService = httpService;
        }
        async Task<IList<AssetAllocationTabs>> IAssetAllocationService.GetAssetAllocationTabs()
        {
            return await _httpService.GetAsync<IList<AssetAllocationTabs>>($"{_baseURL}/GetAssetAllocationTabs");
        }
        async Task<AssetAllocation> IAssetAllocationService.GetAssetAllocationList(int Tabindex, int SearchId, int AssetCategoryCode,Filter Filter)
        {            
            return await _httpService.PostAsync<AssetAllocation,Filter>($"{_baseURL}/GetAssetAllocationList?Tabindex={Tabindex}&SearchId={SearchId}&AssetCategoryCode={AssetCategoryCode}",Filter);
        }
        async Task<AssetAllocation> IAssetAllocationService.GetAssetAllocationDetails(Guid AssetAllocationGI)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetAssetAllocationDetails?AssetAllocationGI={AssetAllocationGI}");
        }

        async Task<AssetAllocation> IAssetAllocationService.GetEmpAssetReqDetails(Guid EmployeeGI,   int TrnCode)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetEmpAssetReqDetails?EmployeeGI={EmployeeGI}&TrnCode={TrnCode}");
        }
        async Task<AssetAllocation> IAssetAllocationService.GetEmployeeCard(int EmpCode)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetEmployeeCard?EmpCode={EmpCode}");
        }
        async Task<AssetAllocation> IAssetAllocationService.GetIssueDetails(Guid AssetAllocationGI)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetIssueDetails?AssetAllocationGI={AssetAllocationGI}");
        }
        async Task<AssetAllocation> IAssetAllocationService.GetAssetDetails(int AssetCode)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetAssetDetails?AssetCode={AssetCode}");
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetAllocationDetailsSave(AssetAllocation model)
        {            
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/AssetAllocationDetailsSave", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetAllocationDelete(Guid AssetAllocationGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/AssetAllocationDetailsDelete?AssetAllocationGI={AssetAllocationGI}");
        }
        async Task<IList<AssetLog>> IAssetAllocationService.GetAssetLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<AssetLog>>($"{_baseURL}/GetAssetLogList?EmployeeGI={EmployeeGI}");
        }
        async Task<AssetAllocation> IAssetAllocationService.GetAssetLogDetails(Guid AssetAllocationGI)
        {
            return await _httpService.GetAsync<AssetAllocation>($"{_baseURL}/GetAssetLogDetails?AssetAllocationGI={AssetAllocationGI}");
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetReturn(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/AssetReturn",model);
        }
        async Task<ResponseEntity> IAssetAllocationService.NewRequest(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/NewRequest", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetAllocationApprove(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/AssetAllocationApprove", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetAllocationSave(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/AssetAllocationSave", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.AssetIssue(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/AssetIssue", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.Approve(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/Approve", model);
        }
        async Task<ResponseEntity> IAssetAllocationService.Reject(AssetAllocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetAllocation>($"{_baseURL}/Reject", model);
        }
    }
}
