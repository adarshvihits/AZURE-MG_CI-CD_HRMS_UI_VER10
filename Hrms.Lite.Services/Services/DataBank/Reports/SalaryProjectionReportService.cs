using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SalaryProjectionReportService : ISalaryProjectionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryProjectionReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryProjectionReport";
            _httpService = httpService;
        }


        public async Task<DataSet> GetList(int? AmountType, int? Type, string Amount, string Allowance, SalaryProjectionReport model)
        {
            return await _httpService.PostAsync<DataSet, SalaryProjectionReport>($"{_baseURL}/GetList?AmountType={AmountType}&Type={Type}&Amount={Amount}&Allowance={Allowance}", model);
        }


    }
}
