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
    public class VacancyManagementService: IVacancyManagementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VacancyManagementService(IServiceBase httpService)
        {
            _baseURL = "VacancyManagement";
            _httpService = httpService;
        }
        public async Task<VacancyManagement> GetList(int Tabindex)
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetVacancyManagementList?Tabindex={Tabindex}");
        }
        public async Task<VacancyManagement> GetGraphDetails()
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetGraphDetails");
        }
        public async Task<VacancyManagement> GetGraphDetailsRequirements()
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetGraphDetailsRequirements");
        }
        public async Task<VacancyManagementApproval> GetVacancyManagementApprovalList()
        {
            return await _httpService.GetAsync<VacancyManagementApproval>($"{_baseURL}/GetVacancyManagementApprovalList");
        }
        public async Task<VacancyManagement> GetVacancyManagementDetails(Guid VacancyManagementGI)
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetVacancyManagementDetails?VacancyManagementGI={VacancyManagementGI}");
        }
        public async Task<VacancyManagement> GetRefNo()
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetRefNo");
        }
        public async Task<VacancyManagement> GetDesignationVSJD(int DesignationCode)
        {
            return await _httpService.GetAsync<VacancyManagement>($"{_baseURL}/GetDesignationVSJD?DesignationCode={DesignationCode}");
        }
        public async Task<EmployeeDT> GetVacancyTypePopUp(Guid VacancyGI)
        {
            return await _httpService.GetAsync<EmployeeDT>($"{_baseURL}/GetResignedEmployeeDetails?VacancyManagementGI={VacancyGI}");
        }
        public async Task<ResponseEntity> SaveVacancyManagemenSave(VacancyManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, VacancyManagement>($"{_baseURL}/VacancyManagementSave", model);
        }
        public async Task<ResponseEntity> ApproveReject(VacancyManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, VacancyManagement>($"{_baseURL}/Approve", model);
        }
        public async Task<ResponseEntity> FilledVacancySave(VacancyManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, VacancyManagement>($"{_baseURL}/FilledVacancySave", model);
        }
        public async Task<ResponseEntity> DeleteVacancy(VacancyManagement model)
        {
            return await _httpService.PostAsync<ResponseEntity, VacancyManagement>($"{_baseURL}/VacancyDelete", model);
        }
        public async Task<ResponseEntity> GetVacancyCancel(Guid VacancyManagementGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetVacancyCancel?VacancyManagementGI={VacancyManagementGI}");
        }
    }
}
