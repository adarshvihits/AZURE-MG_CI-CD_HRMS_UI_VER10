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
    public class HRPolicyAcceptanceReportService : IHRPolicyAcceptanceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRPolicyAcceptanceReportService(IServiceBase httpService)
        {
            _baseURL = "HRPolicyAcceptanceReport";
            _httpService = httpService;
        }

        public async Task<HRPolicyAcceptanceReport> GetPolicyAcceptanceListReport(HRPolicyAcceptanceReport model)
        {
            return await _httpService.PostAsync<HRPolicyAcceptanceReport, HRPolicyAcceptanceReport>($"{_baseURL}/GetPolicyAcceptanceListReport", model);
        }
        public async Task<HRPolicyAcceptanceReport> GetGroupSummary(HRPolicyAcceptanceReport model)
        {
            return await _httpService.PostAsync<HRPolicyAcceptanceReport, HRPolicyAcceptanceReport>($"{_baseURL}/GetGroupSummary", model);
        }
        public async Task<DataSet> PolicyAcceptanceListReport_Excel(HRPolicyAcceptanceReport model)
        {
            return await _httpService.PostAsync<DataSet, HRPolicyAcceptanceReport>($"{_baseURL}/PolicyAcceptanceListReport_Excel", model);

        }
        //-------------------------------------------
        public async Task<HrPolicy_AcceptanceReport> GetHrPolicyAcceptanceDetails(int PolicyCode, Filter filter)
        {

            return await _httpService.PostAsync<HrPolicy_AcceptanceReport, Filter>($"{_baseURL}/GetHrPolicyAcceptanceDetails?PolicyCode={PolicyCode}", filter);

        }
        public async Task<DataSet> HrPolicyAcceptance_Excel(Filter filter, int PolicyCode)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/HrPolicyAcceptance_Excel?PolicyCode={PolicyCode}", filter);

        }
    }
}
