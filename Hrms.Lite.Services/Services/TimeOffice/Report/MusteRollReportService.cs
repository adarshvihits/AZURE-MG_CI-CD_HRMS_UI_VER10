using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class MusteRollReportService : IMusteRollReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MusteRollReportService(IServiceBase httpService)
        {
            _baseURL = "MusterRollReport";
            _httpService = httpService;
        }
        public async Task<MusterRollReport> GetMusterRollReport(MusterRollReport model)
        {
            return await _httpService.PostAsync<MusterRollReport, MusterRollReport>($"{_baseURL}/GetMusterRollReport", model);
        }
        public async Task<DataSet> GetMusterRollReport_Excel(MusterRollReport model)
        {
            return await _httpService.PostAsync<DataSet, MusterRollReport>($"{_baseURL}/GetMusterRollReport_Excel", model);
        }

        public async Task<DataSet> GetMusterRollReportDynamic(MusterRollReport model)
        {
            return await _httpService.PostAsync<System.Data.DataSet, MusterRollReport>($"{_baseURL}/GetMusterRollReportDynamic", model);
        }

        public async Task<DataSet> GetMusterRollReportHeaders(MusterRollReport model)
        {
            return await _httpService.PostAsync<System.Data.DataSet, MusterRollReport>($"{_baseURL}/GetMusterRollReportHeaders", model);
        }

    }
}
