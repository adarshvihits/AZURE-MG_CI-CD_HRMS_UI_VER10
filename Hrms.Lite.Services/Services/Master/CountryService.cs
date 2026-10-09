using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class CountryService : ICountryService
    {
       
            private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
            public CountryService(IServiceBase httpService)
            {
                _baseURL = "Country";
            _baseURL1 = "Common";
            _httpService = httpService;
            }

        public Task<ResponseEntity> ApproveOrReject(Country model)
        {
            return _httpService.PostAsync<ResponseEntity, Country>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Country input)
        {
            return _httpService.PostAsync<ResponseEntity, Country>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(Country model)
        {
            return await _httpService.PostAsync<ResponseEntity, Country>($"{_baseURL}/Delete",model);
        
        }

        public Task<Country> GetCountryDetails(Guid CountryGI,Guid LogGI,string Type)
        {
            return _httpService.GetAsync<Country>($"{_baseURL}/GetCountryDetails?CountryGI={CountryGI}&LogGi={LogGI}&Type={Type}");
        }

        public async Task<Country> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Country>($"{_baseURL}/list?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
