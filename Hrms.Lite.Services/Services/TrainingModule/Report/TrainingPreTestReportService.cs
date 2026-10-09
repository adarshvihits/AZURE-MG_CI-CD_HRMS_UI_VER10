using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule.Report
{
   public class TrainingPreTestReportService :ITrainingPreTestReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingPreTestReportService(IServiceBase httpService)
        {
            _baseURL = "TrainingPreTestReport";
            _httpService = httpService;
        }
      
        public async Task<TrainingPreTestReport> GetTrainingPreTestReportList(int Training, Filter model)
        {
            return await _httpService.PostAsync<TrainingPreTestReport, Filter>($"{_baseURL}/GetReport?Code={Training}", model);
        }

        public async Task<DataSet> GetTrainingPreTestReport_Excel(Filter filter, int Training)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetTrainingPreTestReport_Excel?Code={Training}", filter);

        }

    }
}
