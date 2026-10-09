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
  public  class SalaryReportDraftService: ISalaryReportDraftService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReportDraftService(IServiceBase httpService)
        {
            _baseURL = "SalaryReportDraft";
            _httpService = httpService;
        }
        public async Task<SalaryReportDraft> GetList(int Month, int PayrollGrp, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReportDraft, Filter>($"{_baseURL}/GetSalaryReportDraft?Month={Month}&PayrollGrp={PayrollGrp}", filter);
        }
        public async Task<NetPaySplitUp> GetNetPaySplitUpDetails(Guid EmployeeGI, int SalaryPeriod, int PayrollGroup)
        {
            return await _httpService.GetAsync<NetPaySplitUp>($"{_baseURL}/GetNetPaySplitUpDetails?EmployeeGI={EmployeeGI}&SalaryPeriod={SalaryPeriod}&PayrollGroup={PayrollGroup}");

        }
        public async Task<DataSet> GetSalaryReportDraft_Excel(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryReportDraft_Excel?Month={Month}&PayrollGrp={PayrollGroup}", filter);
        }

    }
}
