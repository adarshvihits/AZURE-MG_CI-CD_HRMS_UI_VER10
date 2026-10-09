using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class EConsoleSubmissionReportService: IEConsoleSubmissionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EConsoleSubmissionReportService(IServiceBase httpService)
        {
            _baseURL = "EConsoleSubmissionReport";
            _httpService = httpService;
        }
        public async Task<EConsoleSubmissionReport> GetList(int ConsoleType, Filter filter)
        {
            return await _httpService.PostAsync<EConsoleSubmissionReport, Filter>($"{_baseURL}/GetList?ConsoleType={ConsoleType}", filter);
        }

        public async Task<DataSet> EConsoleSubmissionreport_Excel(int TypeCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EConsoleSubmissionreport_Excel?TypeCode={TypeCode}", filter);
        }
    }
}


    
