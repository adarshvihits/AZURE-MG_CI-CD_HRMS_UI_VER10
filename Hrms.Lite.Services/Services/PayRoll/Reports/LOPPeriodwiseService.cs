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
    public class LOPPeriodwiseService : ILOPPeriodwiseService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LOPPeriodwiseService(IServiceBase httpService)
        {
            _baseURL = "LOPPeriodwiseRpt";
            _httpService = httpService;
        }
        public async Task<LOPPeriodwise> GetList(int fmonth, int tmonth, Filter filter)
        {
            return await _httpService.PostAsync<LOPPeriodwise, Filter>($"{_baseURL}/GetReportList?fmonth={fmonth}&tmonth={tmonth}", filter);
        }

        public async Task<DataSet> GetLOPReport_Excel(int fmonth, int tmonth, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLOPReport_Excel?fmonth={fmonth}&tmonth={tmonth}", filter);

        }
    }
}
