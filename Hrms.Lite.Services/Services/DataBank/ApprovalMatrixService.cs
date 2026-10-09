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
   public class ApprovalMatrixService: IApprovalMatrixService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ApprovalMatrixService(IServiceBase httpService)
        {
            _baseURL = "ApprovalMatrix";
            _httpService = httpService;
        }
        public async Task<ApprovalMatrix> ApprovalMatrixList(int WorkFlow, Filter filter)
        {
            return await _httpService.PostAsync<ApprovalMatrix, Filter>($"{_baseURL}/ApprovalMatrixList?WorkFlow={WorkFlow}", filter);
        }
        public async Task<ApprovalMatrix> GetRoleList(ApprovalMatrix model)
        {
            return await _httpService.PostAsync<ApprovalMatrix, ApprovalMatrix>($"{_baseURL}/GetRoleList",model);
        }
        public async Task<ApprovalMatrix> GetEditRoleList(int? NoOfLevels, Guid ApprovalMatrixGI)
        {
            return await _httpService.GetAsync<ApprovalMatrix>($"{_baseURL}/GetEditRoleList?NoOfLevels={NoOfLevels}&ApprovalMatrixGI={ApprovalMatrixGI}");
        }
        public async Task<ResponseEntity> ApprovalMatrixSave(ApprovalMatrix model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApprovalMatrix>($"{_baseURL}/ApprovalMatrixSave", model);
        }
        public async Task<ResponseEntity> ApprovalMatrixUpdate(ApprovalMatrix model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApprovalMatrix>($"{_baseURL}/ApprovalMatrixUpdate", model);
        }
        public async Task<ApprovalMatrix> GetApprovalMatrixDetails(Guid ApprovalMatrixGI)
        {
            return await _httpService.GetAsync<ApprovalMatrix>($"{_baseURL}/GetApprovalMatrixDetails?ApprovalMatrixGI={ApprovalMatrixGI}");
        }
        public async Task<ApprovalMatrix> GetApprovalMatrixEditDetails(Guid ApprovalMatrixGI)
        {
            return await _httpService.GetAsync<ApprovalMatrix>($"{_baseURL}/GetApprovalMatrixEditDetails?ApprovalMatrixGI={ApprovalMatrixGI}");
        }
        public async Task<DataSet> GetList_Excel(int WorkFlow, Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?WorkFlow={WorkFlow}", model);
        }
        public async Task<ResponseEntity> ApprovalMatrixDelete(Guid ApprovalMatrixGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ApprovalMatrixDelete?ApprovalMatrixGI={ApprovalMatrixGI}");
        }
        public async Task<ResponseEntity> ApprovalMatrixLevelDelete(Guid ApprovalMatrixGI,int Level)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ApprovalMatrixLevelDelete?ApprovalMatrixGI={ApprovalMatrixGI}&Level={Level}");
        }
        public async Task<List<Dropdown>> GetAuthorityEntityDropdown(ApprovalMatrix model)
        {
            return await _httpService.PostAsync<List<Dropdown>, ApprovalMatrix>($"{_baseURL}/GetAuthorityEntityDropdown", model);
        }
    }
}
