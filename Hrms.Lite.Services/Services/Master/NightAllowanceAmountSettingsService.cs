using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class NightAllowanceAmountSettingsService: INightAllowanceAmountSettingsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public NightAllowanceAmountSettingsService(IServiceBase httpService)
        {
            _baseURL = "NightAllowanceAmountSettings";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<NightAllowanceAmountSettings> GetList(int DepartmentCode, int PurposeCode, int GradeCode)
        {
            return await _httpService.GetAsync<NightAllowanceAmountSettings>($"{_baseURL}/List?DepartmentCode={DepartmentCode}&&PurposeCode={PurposeCode}&&GradeCode={GradeCode}");
        }

        public Task<ResponseEntity> Create(NightAllowanceAmountSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, NightAllowanceAmountSettings>($"{_baseURL}/Create", input);
        }

        public async Task<NightAllowanceAmountSettings> GetDetails(int Code,int DepartmentCode, int PurposeCode, int GradeCode, string Type)
        {
            return await _httpService.GetAsync<NightAllowanceAmountSettings>($"{_baseURL}/NAASDetails?Code={Code}&&DepartmentCode={DepartmentCode}&&PurposeCode={PurposeCode}&&GradeCode={GradeCode}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(NightAllowanceAmountSettings model)
        {
            return _httpService.PostAsync<ResponseEntity, NightAllowanceAmountSettings>($"{_baseURL}/Delete", model);
        }

    }

}
