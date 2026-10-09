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
	public class ExitCheckListReportService: IExitCheckListReportService
	{
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ExitCheckListReportService(IServiceBase httpService)
        {
            _baseURL = "ExitCheckListReport";
            _httpService = httpService;
        }
        public async Task<ExitCheckListReport> GetExitCheckListReport(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<ExitCheckListReport, Filter>($"{_baseURL}/GetExitCheckListReport?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> GetExitCheckListReport_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExitCheckListReport_Excel?FromDate={FromDate}&ToDate={ToDate}", filter);
        }

		public async Task<ExitCheckListReport> GetExitCheckListDetails(int EmployeeCode)
		{
			return await _httpService.GetAsync<ExitCheckListReport>($"{_baseURL}/GetExitCheckListDetails?EmployeeCode={EmployeeCode}");

		}
        public async Task<DataSet> GetExitCheckListPopUp_Excel(int EmployeeCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetExitCheckListPopUp_Excel?EmployeeCode={EmployeeCode}");
        }
    }
}
