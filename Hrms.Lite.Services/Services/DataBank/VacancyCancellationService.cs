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
   public class VacancyCancellationService:IVacancyCancellationService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VacancyCancellationService(IServiceBase httpService)
        {
            _baseURL = "VacancyCancellation";
            _httpService = httpService;
        }

        public async Task<VacancyCancellation> GetCancellationDetails(Guid VacancycancellationGI)
        {
            return await _httpService.GetAsync<VacancyCancellation>($"{_baseURL}/GetVacancyCancellationDetails?VacancyManagementGI={VacancycancellationGI}");
        }

        public  Task<ResponseEntity> Cancel(VacancyCancellation model)
        {
            return _httpService.PostAsync<ResponseEntity, VacancyCancellation>($"{_baseURL}/Cancel", model);
        }

        public async Task<VacancyCancellation> GetList()
        {
            return await _httpService.GetAsync<VacancyCancellation>($"{_baseURL}/GetVacancyManagementList");
        }
    }
}
