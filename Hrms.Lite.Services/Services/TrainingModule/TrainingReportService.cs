using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
    public class TrainingReportService : ITrainingReportServices
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingReportService(IServiceBase httpService)
        {
            _baseURL = "TrainingReport";
            _httpService = httpService;
        }
        public async Task<TrainingReport> GetTrainingReportList(int TrainingCode, int FYCode)
        {
            return await _httpService.GetAsync<TrainingReport>($"{_baseURL}/GetTrainingReportList?TrainingCode={TrainingCode}&FYCode={FYCode}");
        }
        //public async Task<TrainingReport> GetEmployeeList(int ScheduleCode, Filter filter)
        //{
        //    return await _httpService.PostAsync<TrainingReport, Filter>($"{_baseURL}/GetEmployeeList?ScheduleCode={ScheduleCode}", filter);
        //}
    }
}
