using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class TrnScheduleRptService: ITrnScheduleRptService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrnScheduleRptService(IServiceBase httpService)
        {
            _baseURL = "TrnScheduleRpt";
            _httpService = httpService;
        }
        public async Task<TrainingSchedule> GetDashboardCountList(int FinancialYearCode)
        {
            return await _httpService.GetAsync<TrainingSchedule>($"{_baseURL}/GetDashboardCountList?FinancialYearCode={FinancialYearCode}");
        }
        public async Task<DataSet> GetTrnScheduleRptList( int FinancialYearCode, int Category)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetTrnScheduleRptList?FinancialYearCode={FinancialYearCode}&Category={Category}");
        }
    }
}
