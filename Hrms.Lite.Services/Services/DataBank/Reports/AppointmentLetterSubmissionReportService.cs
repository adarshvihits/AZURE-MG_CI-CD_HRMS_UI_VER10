using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class AppointmentLetterSubmissionReportService: IAppointmentLetterSubmissionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public AppointmentLetterSubmissionReportService(IServiceBase httpService)
        {
            _baseURL = "AppointmentLetterSubmissionReport";
            _httpService = httpService;
        }
        public async Task<AppointmentLetterSubmissionReport> GetList(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<AppointmentLetterSubmissionReport, Filter>($"{_baseURL}/GetList?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> AppointmentLetterSubmissionReport_Excel(string FromDate, string ToDate, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetAppointmentLetterSubmissionReport_Excel?FromDate={FromDate}&ToDate={ToDate}", Filter);

        }
    }
}
