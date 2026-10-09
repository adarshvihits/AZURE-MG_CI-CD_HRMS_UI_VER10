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
    public class VacancyManagementApprovalService: IVacancyManagementApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public VacancyManagementApprovalService(IServiceBase httpService)
        {
            _baseURL = "VacancyManagementApproval";
            _httpService = httpService;
        }
        public async Task<VacancyManagementApproval> GetVacancyManagementList()
        {
            return await _httpService.GetAsync<VacancyManagementApproval>($"{_baseURL}/GetVacancyManagementList");
        }
        public async Task<VacancyManagementApproval> GetVacancyManagementDetails(Guid VacancyManagementGI)
        {
            return await _httpService.GetAsync<VacancyManagementApproval>($"{_baseURL}/GetVacancyManagementDetails?VacancyManagementGI={VacancyManagementGI}");
        }
        public async Task<ResponseEntity> VacancyManagementApprove(VacancyManagementApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, VacancyManagementApproval>($"{_baseURL}/VacancyManagementApprove", model);
        }
    }
}
