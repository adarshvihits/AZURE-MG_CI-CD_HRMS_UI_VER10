using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class MonthlyProfitBasedSettingsService: IMonthlyProfitBasedSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyProfitBasedSettingsService(IServiceBase httpService)
        {
            _baseURL = "MonthlyProfitBasedSettings";
            _httpService = httpService;
        }
        public async Task<MonthlyProfitBasedSettings> GetList(int Month)
        {
            return await _httpService.GetAsync<MonthlyProfitBasedSettings>($"{_baseURL}/GetList?Month={Month}");
        }
        public async Task<DataSet> GetList_Excel(int Month)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?Month={Month}");
        }
        public async Task<ResponseEntity<List<MonthlyProfitBasedSettings>>> ImportFromExcel(MonthlyProfitBasedSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<MonthlyProfitBasedSettings>>, MonthlyProfitBasedSettings>($"{_baseURL}/ImportFromExcel", model);
        }
    }
}
