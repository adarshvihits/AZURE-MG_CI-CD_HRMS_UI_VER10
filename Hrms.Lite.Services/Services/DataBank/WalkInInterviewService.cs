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
    public class WalkInInterviewService : IWalkInInterviewService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public WalkInInterviewService(IServiceBase httpService)
        {
            _baseURL = "WalkInInterView";
            _httpService = httpService;
        }
        public async Task<WalkInInterView> GetVacancyList()
        {
            return await _httpService.GetAsync<WalkInInterView>($"{_baseURL}/GetVacancyList");
        }
        public async Task<WalkInInterView> GetVacancyManagementDetails(Guid VacancyManagementGI)
        {
            return await _httpService.GetAsync<WalkInInterView>($"{_baseURL}/GetVacancyManagementDetails?VacancyManagementGI={VacancyManagementGI}");
        }
        public async Task<ResponseEntity> WalkInInterViewSave(WalkInInterView model)
        {
            return await _httpService.PostAsync<ResponseEntity, WalkInInterView>($"{_baseURL}/WalkInInterViewSave", model);
        }
    }
}
