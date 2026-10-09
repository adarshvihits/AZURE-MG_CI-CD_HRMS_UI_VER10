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
    public class TrainingPostTestReportService : ITrainingPostTestReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingPostTestReportService(IServiceBase httpService)
        {
            _baseURL = "TrainingPostTestReport";
            _httpService = httpService;
        }
        public async Task<TrainingPostTestReport> GetTrainingPostTestReportList(int Code, Filter Filter)
        {
            return await _httpService.PostAsync<TrainingPostTestReport, Filter>($"{_baseURL}/GetTrainingPostTestReportList?Code={Code}", Filter);
        }

        public async Task<DataSet> GetTrainingPostTestReportList_Excel(Filter Filter, int Code)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetTrainingPostTestReport_Excel?Code={Code}", Filter);

        }
    }
}
