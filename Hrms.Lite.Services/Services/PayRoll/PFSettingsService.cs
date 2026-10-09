using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class PFSettingsService: IPFSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PFSettingsService(IServiceBase httpService)
        {
            _baseURL = "PFSettings";
            _httpService = httpService;
        }



        public async Task<DataSet> GetPFEmployeeWiseSettingsList_Excel(int DeductionCode, int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPFEmployeeWiseSettingsList_Excel?DeductionCode={DeductionCode}&MonthCode={MonthCode}", filter);
        }
        public async Task<PFSettings> GetPFEmployeeWiseSettingsList(int deduction, int monthcode,int PFGroup,Filter filter)
        {
        return await _httpService.PostAsync<PFSettings,Filter>($"{_baseURL}/GetPFEmployeeWiseSettingsList?DeductionCode={deduction}&MonthCode={monthcode}&PFGroup={PFGroup}",filter);
        }
        public async Task<PFSettings> GetPFSettingsApplicableAllowanceList(int deduction,int pfgroup)
        {
            return await _httpService.GetAsync<PFSettings>($"{_baseURL}/GetPFSettingsApplicableAllowanceList?DeductionCode={deduction}&pfgroup={pfgroup}");
        }
        public async Task<PFSettings> GetPFGeneralsettingsList(int deduction)
        {
            return await _httpService.GetAsync<PFSettings>($"{_baseURL}/GetPFGeneralsettingsList?DeductionCode={deduction}");
        }
        public async Task<PFSettings> GetPFEmployeeWiseSettingsDetails(Guid EmployeeGI, int deduction, int monthcode,int pfgroup)
        {
            return await _httpService.GetAsync<PFSettings>($"{_baseURL}/GetPFEmployeeWiseSettingsDetails?EmployeeGI={EmployeeGI}&DeductionCode={deduction}&MonthCode={monthcode}&pfgroup={pfgroup}");
        }
        public async Task<ResponseEntity> PFEmployeewisesettingsSave(PFSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PFSettings>($"{_baseURL}/PFGEmployeewisesettingsSave", model);
        }
        public async Task<ResponseEntity> GeneralSettingsSave(PFSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PFSettings>($"{_baseURL}/PFGeneralsettingsSave", model);
        }
        public async Task<ResponseEntity> ApplicableAllowanceSave(PFSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PFSettings>($"{_baseURL}/PFSettingsAllowanceSave", model);
        }
    }
}
