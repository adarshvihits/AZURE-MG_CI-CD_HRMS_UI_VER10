using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class ResignationService : IResignationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ResignationService(IServiceBase httpService)
        {
            _baseURL = "Resignation";
            _httpService = httpService;
        }
        async Task<IList<ResignationListTabs>> IResignationService.GetResignationTabs()
        {
            return await _httpService.GetAsync<IList<ResignationListTabs>>($"{_baseURL}/GetTrainingTabs");
        }
        async Task<Resignation> IResignationService.GetResignationList(int Tabindex,Filter filter)
        {
            return await _httpService.PostAsync<Resignation,Filter>($"{_baseURL}/GetResignationList?Tabindex={Tabindex}",filter);
        }
        async Task<IList<ApprovalPendingDetails>> IResignationService.GetPendingApprovalsList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<ApprovalPendingDetails>>($"{_baseURL}/GetPendingApprovalsList?EmployeeGI={EmployeeGI}");
        }
        async Task<Resignation> IResignationService.GetNoticePeriodDetails(Guid EmployeeGI, string ResignationDate, string LastWorkingDay)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetNoticePeriodDetails?EmployeeGI={EmployeeGI}&ResignationDate={ResignationDate}&LastWorkingDay={LastWorkingDay}");
        }
        async Task<Resignation> IResignationService.GetResignationAcceptanceDetails(Guid ResignationGI)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetResignationAcceptanceDetails?ResignationGI={ResignationGI}");
        }
        async Task<Resignation> IResignationService.GetExitChecklistDetail(Guid ResignationGI)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetExitChecklistDetail?ResignationGI={ResignationGI}");
        }
        async Task<Resignation> IResignationService.GetRelievingDetails(Guid ResignationGI)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetRelievingDetails?ResignationGI={ResignationGI}");
        }
       public async Task<ResignationTracker> GetRelievingTracker(Guid ResignationGI)
        {
            return await _httpService.GetAsync<ResignationTracker>($"{_baseURL}/GetRelievingTracker?ResignationGI={ResignationGI}");
        }
        async Task<ResponseEntity> IResignationService.NewRequestSave(Resignation model)
        {
            return await _httpService.PostAsync<ResponseEntity, Resignation>($"{_baseURL}/NewRequestSave", model);
        }
        async Task<ResponseEntity> IResignationService.EmployeeRelievingSave(Resignation model)
        {
            return await _httpService.PostAsync<ResponseEntity, Resignation>($"{_baseURL}/EmployeeRelievingSave", model);
        }
        async Task<ResponseEntity> IResignationService.ExitCheck(Resignation model)
        {
            return await _httpService.PostAsync<ResponseEntity, Resignation>($"{_baseURL}/ExitCheck", model);
        }
        async Task<ResponseEntity> IResignationService.RequestAccepatance(Resignation model)
        {
            return await _httpService.PostAsync<ResponseEntity, Resignation>($"{_baseURL}/RequestAccepatance", model);
        }
        public async Task<Resignation> GetResignationDeleteList(Filter filter)
        {
            return await _httpService.PostAsync<Resignation, Filter>($"{_baseURL}/GetDeleteList", filter);
        }
        public async Task<Resignation> GetResignationDeleteDetails(Guid EmployeeGI, Guid ResignationGI)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetDeleteDetails?EmployeeGI={EmployeeGI}&ResignationGI={ResignationGI}");

        }
        public async Task<ResponseEntity> Delete(Guid ResignationGI, string Remarks)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/ResignationDelete?ResignationGI={ResignationGI}&Remarks={Remarks}");
        }

        public async Task<Resignation> GetResignationAmendmentDetails(Guid ResignationGI)
        {
            return await _httpService.GetAsync<Resignation>($"{_baseURL}/GetResignationAmendmentDetails?ResignationGI={ResignationGI}");

        }
        async Task<ResponseEntity> IResignationService.SaveResignationAmendment(Resignation model)
        {
            return await _httpService.PostAsync<ResponseEntity, Resignation>($"{_baseURL}/SaveResignationAmendment", model);
        }

    }
}
