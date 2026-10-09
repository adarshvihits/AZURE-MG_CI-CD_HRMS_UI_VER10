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
    public class RecruitmentMatrixService : IRecruitmentMatrixService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RecruitmentMatrixService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentMatrix";
            _httpService = httpService;
        }
        public async Task<RecruitmentMatrix> RecruitmentMatrixList(int TemplateList, Filter filter)
        {
            return await _httpService.PostAsync<RecruitmentMatrix, Filter>($"{_baseURL}/RecruitmentMatrixList?TemplateList={TemplateList}", filter);
        }
        public async Task<RecruitmentMatrix> GetRoleList(RecruitmentMatrix model)
        {
            return await _httpService.PostAsync<RecruitmentMatrix, RecruitmentMatrix>($"{_baseURL}/GetRoleList", model);
        }
        public async Task<RecruitmentMatrix> GetEditRoleList(int? NoOfLevels, Guid RecruitmentMatrixGI)
        {
            return await _httpService.GetAsync<RecruitmentMatrix>($"{_baseURL}/GetEditRoleList?NoOfLevels={NoOfLevels}&RecruitmentMatrixGI={RecruitmentMatrixGI}");
        }
        public async Task<ResponseEntity> RecruitmentMatrixSave(RecruitmentMatrix model)
        {
            return await _httpService.PostAsync<ResponseEntity, RecruitmentMatrix>($"{_baseURL}/RecruitmentMatrixSave", model);
        }
        public async Task<ResponseEntity> RecruitmentMatrixUpdate(RecruitmentMatrix model,int Template)
        {
            return await _httpService.PostAsync<ResponseEntity, RecruitmentMatrix>($"{_baseURL}/RecruitmentMatrixUpdate?Template={Template}", model);
        }
        public async Task<RecruitmentMatrix> GetRecruitmentMatrixDetails(Guid RecruitmentMatrixGI)
        {
            return await _httpService.GetAsync<RecruitmentMatrix>($"{_baseURL}/GetRecruitmentMatrixDetails?RecruitmentMatrixGI={RecruitmentMatrixGI}");
        }
        public async Task<RecruitmentMatrix> GetRecruitmentMatrixEditDetails(int TemplateList)
        {
            return await _httpService.GetAsync<RecruitmentMatrix>($"{_baseURL}/GetRecruitmentMatrixEditDetails?TemplateList={TemplateList}");
        }
        public async Task<DataSet> GetList_Excel(int WorkFlow, Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?WorkFlow={WorkFlow}", model);
        }
        public async Task<ResponseEntity> RecruitmentMatrixDelete(Guid RecruitmentMatrixGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/RecruitmentMatrixDelete?RecruitmentMatrixGI={RecruitmentMatrixGI}");
        }
        public async Task<ResponseEntity> RecruitmentMatrixLevelDelete(Guid RecruitmentMatrixGI, int Level)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/RecruitmentMatrixLevelDelete?RecruitmentMatrixGI={RecruitmentMatrixGI}&Level={Level}");
        }
        public async Task<List<Dropdown>> GetAuthorityEntityDropdown(RecruitmentMatrix model)
        {
            return await _httpService.PostAsync<List<Dropdown>, RecruitmentMatrix>($"{_baseURL}/GetAuthorityEntityDropdown", model);
        }
    }
}
