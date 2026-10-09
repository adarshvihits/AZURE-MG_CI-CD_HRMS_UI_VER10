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
   public class VacancyClosingService : IVacancyClosingService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VacancyClosingService(IServiceBase httpService)
        {
            _baseURL = "VacancyClosing";
            _httpService = httpService;
        }

        public async Task<VacancyCancellation> GetVacancyClosingDetails(Guid VacancycancellationGI)
        {
            return await _httpService.GetAsync<VacancyCancellation>($"{_baseURL}/GetVacancyClosingDetails?VacancyManagementGI={VacancycancellationGI}");
        }

        public  Task<ResponseEntity> VacancyClose(VacancyCancellation model)
        {
            return _httpService.PostAsync<ResponseEntity, VacancyCancellation>($"{_baseURL}/VacancyClose", model);
        }

        public async Task<VacancyCancellation> GetVacancyClosingList()
        {
            return await _httpService.GetAsync<VacancyCancellation>($"{_baseURL}/GetVacancyClosingList");
        }
    }
}
