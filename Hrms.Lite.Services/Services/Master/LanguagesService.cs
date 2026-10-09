using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class LanguagesService : ILanguagesService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public LanguagesService(IServiceBase httpService)
        {
            _baseURL = "Languages";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        public Task<ResponseEntity> ApproveOrReject(Languages model)
        {
            return _httpService.PostAsync<ResponseEntity, Languages>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Languages input)
        {
            return _httpService.PostAsync<ResponseEntity, Languages>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Languages input)
        {
            return _httpService.PostAsync<ResponseEntity, Languages>($"{_baseURL}/Delete", input);
        }

        public Task<Languages> GetLanguageDetails(Guid LanguageGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Languages>($"{_baseURL}/LanguageDetails?LanguageGI={LanguageGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Languages> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Languages>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
