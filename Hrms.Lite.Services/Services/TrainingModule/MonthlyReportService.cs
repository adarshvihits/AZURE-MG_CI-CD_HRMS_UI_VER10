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
  public class MonthlyReportService: IMonthlyReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyReportService(IServiceBase httpService)
        {
            _baseURL = "MonthlyReport";
            _httpService = httpService;
        }
        public async Task<MonthlyReport> GetMonthlyReportList(int Month)
        {
            return await _httpService.GetAsync<MonthlyReport>($"{_baseURL}/GetMonthlyReportList?Month={Month}");
        }
        public async Task<MonthlyReport> GetParticipantsSelectedDetails(int ScheduleCode, int Month)
        {
            return await _httpService.GetAsync<MonthlyReport>($"{_baseURL}/GetParticipantsSelectedDetails?ScheduleCode={ScheduleCode}&Month={Month}");
        }
        public async Task<MonthlyReport> GetParticipantsCompletedDetails(int ScheduleCode, int Month)
        {
            return await _httpService.GetAsync<MonthlyReport>($"{_baseURL}/GetParticipantsCompletedDetails?ScheduleCode={ScheduleCode}&Month={Month}");
        }

        public async Task<DataSet> ParticipantselectedExcel(int ScheduleCode, int Month)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/ParticipantselectedExcel?ScheduleCode={ScheduleCode}&Month={Month}");
        }
        public async Task<DataSet> ParticipantcompletedExcel(int ScheduleCode, int Month)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/ParticipantcompletedExcel?ScheduleCode={ScheduleCode}&Month={Month}");
        }




    }
}
