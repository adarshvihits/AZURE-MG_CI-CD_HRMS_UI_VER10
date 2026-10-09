using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.Services.Master
{
    public class VersionUpdateService:IVersionUpdateService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public VersionUpdateService(IServiceBase httpService)
        {
            _baseURL = "VersionUpdate";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<VersionUpdate> GetVersionUpdateList(int VersionCode)
        {
            return await _httpService.GetAsync<VersionUpdate>($"{_baseURL}/GetVersionUpdateList?VersionCode={VersionCode}");
        }
        public async Task<ResponseEntity> VersionUpdateSave(VersionUpdate input)
        {
            return await _httpService.PostAsync<ResponseEntity, VersionUpdate>($"{_baseURL}/VersionUpdateSave", input);
        }

        public async Task<VersionUpdate> GetVersionsList()
        {
            return await _httpService.GetAsync<VersionUpdate>($"{_baseURL}/GetVersionsList");
        }
        public async Task<VersionUpdate> GetVersionUpdateDetails(Guid VersionUpdateGI)
        {
            return await _httpService.GetAsync<VersionUpdate>($"{_baseURL}/GetVersionUpdateDetails?VersionUpdateGI="+ VersionUpdateGI);
        }
        public async Task<ResponseEntity> NewVersionSave(List<NewVersion> input)
        {
            return await _httpService.PostAsync<ResponseEntity, List<NewVersion>>($"{_baseURL}/NewVersionSave", input);
        }
        public async Task<ResponseEntity> Create(VersionUpdate input)
        {
            return await _httpService.PostAsync<ResponseEntity, VersionUpdate>($"{_baseURL}/VersionUpdateSave", input);
        }
        public async Task<ResponseEntity<List<VersionUpdate>>> ImportFromExcel(VersionUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VersionUpdate>>, VersionUpdate>($"{_baseURL}/ImportFromExcel", model);

        }
        public Task<ResponseEntity> Delete(Guid GI)
        {
            return _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/VersionUpdateDelete?GI={GI}");
        }
    }
}
