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
    public class StaffConfirmationReportService : IStaffConfirmationReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public StaffConfirmationReportService(IServiceBase httpService)
        {
            _baseURL = "StaffConfirmationReport";
            _httpService = httpService;
        }

        public async Task<StaffConfirmationReport> GetStaffConfirmationList(int Month)
        {
            return await _httpService.GetAsync<StaffConfirmationReport>($"{_baseURL}/GetStaffConfirmationList?Month={Month}");
        }

        public async Task<StaffConfirmationReport> GetStaffConfirmationReportDetails(Guid EmployeeGI,int Month)
        {
            return await _httpService.GetAsync<StaffConfirmationReport>($"{_baseURL}/GetStaffConfirmationReportDetails?EmployeeGI={EmployeeGI}&Month={Month}");
        }

        public async Task<DataSet> GetStaffConfirmationReport_Excel(int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetStaffConfirmationReport_Excel?Month={Month}", filter);
        }

        public async Task<DataSet> GetStaffConfirmationReportDetails_Excel(Guid EmployeeGI, int Month)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetStaffConfirmationReportDetails_Excel?EmployeeGI={EmployeeGI}&Month={Month}");
        }
    }
}
