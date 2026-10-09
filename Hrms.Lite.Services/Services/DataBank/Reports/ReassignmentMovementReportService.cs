
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
    public class ReassignmentMovementReportService : IReassignmentMovementReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ReassignmentMovementReportService(IServiceBase httpService)
        {
            _baseURL = "ReassignmentMovementReport";
            _httpService = httpService;
        }
        public Task<DataSet> GetDetails(ReassignmentMovementReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, ReassignmentMovementReport>($"{_baseURL}/GetDetails", model);
        }

    }
}
