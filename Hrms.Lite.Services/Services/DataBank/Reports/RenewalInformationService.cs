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
    public class RenewalInformationService: IRenewalInformationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RenewalInformationService(IServiceBase httpService)
        {
            _baseURL = "RenewalInformationReport";
            _httpService = httpService;
        }
        public async Task<RenewalInformation> RenewalInformationReport(string fromDate, string toDate)
        {
            return await _httpService.GetAsync<RenewalInformation>($"{_baseURL}/GetGroupingReport?FromDate={fromDate}&ToDate={toDate}");
        }
        public async Task<RenewalInformation> RenewalInformationList(int type,string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<RenewalInformation, Filter>($"{_baseURL}/GetRenewalInformationList?Type={type}&FromDate={fromDate}&ToDate={toDate}", filter);
        }

      public async Task<DataSet> GetRenewalInformationList_Excel(int Type, string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetRenewalInformationList_Excel?Type={Type}&FromDate={FromDate}&ToDate={ToDate}", filter);

        }
    }
}
