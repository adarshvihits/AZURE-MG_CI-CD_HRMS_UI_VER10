using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
	public class LoanIssueReportService: ILoanIssueReportService
	{
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LoanIssueReportService(IServiceBase httpService)
        {
            _baseURL = "LoanIssueReport";
            _httpService = httpService;
        }

        public async Task<LoanIssueReport> GetLoanIssueReport(string FromDate, string ToDate, int type, Filter filter)
        {
            return await _httpService.PostAsync<LoanIssueReport, Filter>($"{_baseURL}/GetLoanIssueReport?FromDate={FromDate}&ToDate={ToDate}&LoanType={type}", filter);
        }
        public async Task<DataSet> GetLoanIssueReport_Excel(string FromDate, string ToDate, int type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLoanIssueReport_Excel?FromDate={FromDate}&ToDate={ToDate}&type={type}", filter);
        }
    }
}
