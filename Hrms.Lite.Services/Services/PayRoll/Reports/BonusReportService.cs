using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class BonusReportService: IBonusReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BonusReportService(IServiceBase httpService)
        {
            _baseURL = "BonusProcessingReport";
            _httpService = httpService;
        }
        public async Task<DataSet> GetBonusProcessingReport(Filter filter, int BonusGroup, int ProcessMonth, int AmountType)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetBonusProcessingReport?BonusGroup={BonusGroup}&ProcessMonth={ProcessMonth}&AmountType={AmountType}", filter);

        }
        public async Task<DataSet> GetBonusProcessingReport_Excel(Filter filter, int BonusGroup, int ProcessMonth, int AmountType)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetBonusProcessingReport_Excel?BonusGroup={BonusGroup}&ProcessMonth={ProcessMonth}&AmountType={AmountType}", filter);

        }
    }
}
