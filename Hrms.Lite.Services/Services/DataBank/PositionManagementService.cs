using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.Services.DataBank
{
   public class PositionManagementService:IPositionManagementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PositionManagementService(IServiceBase httpService)
        {
            _baseURL = "PositionManagement";
            _httpService = httpService;
        }
        public async Task<PositionManagement> PositionManagementInfo()
        {
            return await _httpService.GetAsync<PositionManagement>($"{_baseURL}/PositionManagementInfo");
        }
        public async Task<PositionManagement> GetPositionManagementDetails(string type, Filter input)
        {
            return await _httpService.PostAsync<PositionManagement,Filter>($"{_baseURL}/GetPositionManagementDetails?type={type}", input);
        }
        public async Task<PositionManagement> GetPositionApprovalDetails()
        {
            return await _httpService.GetAsync<PositionManagement>($"{_baseURL}/GetPositionApprovalDetails");
        }
        public async Task<ResponseEntity> GetPositionProceedValidation(PositionManagement input)
        {
            return await _httpService.PostAsync<ResponseEntity, PositionManagement>($"{_baseURL}/GetPositionProceedValidation", input);
        }
        public async Task<PositionManagement> GetPositionDetailsList(int LocationCode, int DepartmentCode, int Designation, int NoofPosition)
        {
            return await _httpService.GetAsync<PositionManagement>($"{_baseURL}/GetPositionDetailsList?LocationCode={LocationCode}&DepartmentCode={DepartmentCode}&Designation={Designation}&NoofPosition={NoofPosition}");
        }
        public async Task<ResponseEntity> Save(PositionManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, PositionManagement>($"{_baseURL}/Save", model);
        }
        public async Task<PositionManagement> GetPositionDetails(Guid PositionGI, int PositionCode, string Type)
        {
            return await _httpService.GetAsync<PositionManagement>($"{_baseURL}/GetPositionDetails?PositionGI={PositionGI}&PositionCode={PositionCode}&Type={Type}");
        }
        public Task<ResponseEntity> Delete(Guid GI)
        {
            return _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?GI={GI}");
        }
        public async Task<ResponseEntity> Approve(PositionManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, PositionManagement>($"{_baseURL}/Approve", model);
        }
        public async Task<DataSet> GetPositionManagementDetails_Excel(string type, Filter input)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPositionManagementDetails_Excel?type={type}", input);
        }
    }
}
