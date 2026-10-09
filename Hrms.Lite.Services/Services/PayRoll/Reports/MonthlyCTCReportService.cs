using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class MonthlyCTCReportService : IMonthlyCTCReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyCTCReportService(IServiceBase httpService)
        {
            _baseURL = "MonthlyCTC";
            _httpService = httpService;
        }

        public Task<System.Data.DataSet> GetMonthlyCTCReport(int SalaryPeriodCode, PaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, PaySheet>($"{_baseURL}/GetMonthlyCTCReport?SalaryPeriodCode={SalaryPeriodCode}", model);
        }
    }
}
