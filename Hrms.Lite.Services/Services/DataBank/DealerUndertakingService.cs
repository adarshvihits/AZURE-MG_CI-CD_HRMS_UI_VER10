using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class DealerUndertakingService : IDealerUndertakingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DealerUndertakingService(IServiceBase httpService)
        {
            _baseURL = "DealerUndertaking";
            _httpService = httpService;
        }
        public async Task<DealerUndertaking> DealerUndertakingList(string TabIndex)
        {
            return await _httpService.GetAsync<DealerUndertaking>($"{_baseURL}/DealerUndertakingList?TabIndex={TabIndex}");
        }
        public async Task<DealerUndertaking> GetDealerUndertaking(Guid EmployeeGI, string TabIndex)
        {
            return await _httpService.GetAsync<DealerUndertaking>($"{_baseURL}/GetDealerUndertaking?EmployeeGI={EmployeeGI}&TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> SaveDealerUndertakingDetails(DealerUndertaking Model)
        {
            return await _httpService.PostAsync<ResponseEntity, DealerUndertaking>($"{_baseURL}/SaveDealerUndertakingDetails", Model);

        }
        public async Task<DealerUndertaking> GetDealerUndertakingDetails(Guid EmployeeGI,int RequestCode, string TabIndex)
        {
            return await _httpService.GetAsync<DealerUndertaking>($"{_baseURL}/GetDealerUndertakingDetails?EmployeeGI={EmployeeGI}&RequestCode={RequestCode}&TabIndex={TabIndex}");
        }
    }
}
