using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class ReassignmentIncentiveReportService : IReassignmentIncentiveReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ReassignmentIncentiveReportService(IServiceBase httpService)
        {
            _baseURL = "ReassignmentIncentiveReport";
            _httpService = httpService;
        }
        public Task<DataSet> GetDetails(ReassignmentIncentiveReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, ReassignmentIncentiveReport>($"{_baseURL}/GetDetails", model);
        }

    }
}
