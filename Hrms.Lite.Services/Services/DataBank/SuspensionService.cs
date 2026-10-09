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

    public class SuspensionService: ISuspensionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SuspensionService(IServiceBase httpService)
        {
            _baseURL = "Suspension";
            _httpService = httpService;
        }
        public async Task<Suspension> GetList(int Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<Suspension, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filter);
        } 
        public async Task<Suspension> GetDetails(Guid IncidentGI)
        {
            return await _httpService.GetAsync<Suspension>($"{_baseURL}/GetDetails?IncidentGI={IncidentGI}");
        }
        public async Task<ResponseEntity> Save(Suspension model)
        {
            return await _httpService.PostAsync<ResponseEntity, Suspension>($"{_baseURL}/Save", model);
        }
        public async Task<ResponseEntity> Delete(Guid IncidentGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?IncidentGI={IncidentGI}");
        }
        public async Task<Suspension> GetTransactionHistoryList(string FromDate, string ToDate,Filter filter)
        {
            return await _httpService.PostAsync<Suspension, Filter>($"{_baseURL}/GetTransactionHistoryList?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
    }
}
