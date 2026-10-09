using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IVacancyManagementService
    {
        public Task<VacancyManagement> GetList(int Tabindex);
        public Task<VacancyManagement> GetGraphDetails();
        public Task<VacancyManagement> GetGraphDetailsRequirements();
        //public Task<VacancyManagement> GetGraphDetailsStatus();
        public Task<VacancyManagementApproval> GetVacancyManagementApprovalList();
        public Task<VacancyManagement> GetVacancyManagementDetails(Guid VacancyManagementGI);
        public Task<VacancyManagement> GetRefNo();
        public Task<VacancyManagement> GetDesignationVSJD(int DesignationCode);
        public Task<EmployeeDT> GetVacancyTypePopUp(Guid VacancyGI);
        public Task<ResponseEntity> SaveVacancyManagemenSave(VacancyManagement model);
        public Task<ResponseEntity> ApproveReject(VacancyManagement model);
        public Task<ResponseEntity> FilledVacancySave(VacancyManagement model);
        public Task<ResponseEntity> DeleteVacancy(VacancyManagement model);
        public Task<ResponseEntity> GetVacancyCancel(Guid VacancyManagementGI);
    }
}
