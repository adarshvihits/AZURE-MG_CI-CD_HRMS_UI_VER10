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
    public class ReaderLogReportService: IReaderLogReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ReaderLogReportService(IServiceBase httpService)
        {
            _baseURL = "ReaderLogReport";
            _httpService = httpService;
        }
        public async Task<ReaderLogReport> GetReaderLogListReport(ReaderLogReport model)
        {
            return await _httpService.PostAsync<ReaderLogReport, ReaderLogReport>($"{_baseURL}/GetReaderLogListReport", model);
        }

       public async Task<DataSet> GetReaderLogListReport_Excel(ReaderLogReport model)
        {
            return await _httpService.PostAsync<DataSet, ReaderLogReport>($"{_baseURL}/GetReaderLogListReport_Excel", model);

        }
    }
}
