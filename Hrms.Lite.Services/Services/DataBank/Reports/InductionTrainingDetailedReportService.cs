using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class InductionTrainingDetailedReportService: IInductionTrainingDetailedReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public InductionTrainingDetailedReportService(IServiceBase httpService)
        {
            _baseURL = "InductionTrainingDetailedReport";
            _httpService = httpService;
        }

        public async Task<InductionTraining> GetList(string From, string To, Filter filter)
        {
            return await _httpService.PostAsync<InductionTraining, Filter>($"{_baseURL}/GetInductionDetailReport?From={From}&To={To}", filter);
        }

        public async Task<DataSet> GetInductionExcel(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetInductionReport_Excel?From={fromDate}&To={toDate}", filter);
        }
    }
}
