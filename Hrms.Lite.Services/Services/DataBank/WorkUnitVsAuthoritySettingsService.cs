using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class WorkUnitVsAuthoritySettingsService: IWorkUnitVsAuthoritySettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public WorkUnitVsAuthoritySettingsService(IServiceBase httpService)
        {
            _baseURL = "WorkUnitVsAuthoritySettings";
            _httpService = httpService;
        }
        public async Task<WorkUnitVsAuthoritySettings> GetList()
        {
            return await _httpService.GetAsync<WorkUnitVsAuthoritySettings>($"{_baseURL}/List");
        }
        public Task<WorkUnitVsAuthoritySettings> GetUnitVsAuthDetails(int WorkunitCode,int RequestTypeCode)
        {
            return _httpService.GetAsync<WorkUnitVsAuthoritySettings>($"{_baseURL}/GetUnitVsAuthDetails?WorkunitCode={WorkunitCode}&RequestTypeCode={RequestTypeCode}");
        }
        public Task<ResponseEntity> Save(WorkUnitVsAuthoritySettings input)
        {
            return _httpService.PostAsync<ResponseEntity, WorkUnitVsAuthoritySettings>($"{_baseURL}/Save", input);
        }
        public Task<WorkUnitVsAuthoritySettings> GetApplicableShift(int WorkunitCode)
        {
            return _httpService.GetAsync<WorkUnitVsAuthoritySettings>($"{_baseURL}/GetApplicableShift?WorkunitCode={WorkunitCode}");
        }
        public Task<ResponseEntity> ApplicableShiftSave(WorkUnitVsAuthoritySettings input)
        {
            return _httpService.PostAsync<ResponseEntity, WorkUnitVsAuthoritySettings>($"{_baseURL}/ApplicableShiftSave", input);
        }
    }
}
