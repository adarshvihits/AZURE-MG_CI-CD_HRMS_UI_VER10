using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{

    public class AbscondingIntimationService : IAbscondingIntimationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AbscondingIntimationService(IServiceBase httpService)
        {
            _baseURL = "AbscondingIntimation";
            _httpService = httpService;
        }
        async Task<AbscondingIntimation> IAbscondingIntimationService.GetList(Filter filter)
        {
            return await _httpService.PostAsync<AbscondingIntimation,Filter>($"{_baseURL}/GetList", filter);
        }
        public async Task<AbscondingIntimation> GetHistoryDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<AbscondingIntimation>($"{_baseURL}/GetHistoryDetails?EmployeeGI={EmployeeGI}");
        }

        public async Task<ResponseEntity> SaveDetag(AbscondingIntimation model)
        {
            return await _httpService.PostAsync<ResponseEntity, AbscondingIntimation>($"{_baseURL}/SaveDetag", model);
        }
    }
}
