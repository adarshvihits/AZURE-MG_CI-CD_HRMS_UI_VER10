using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class ServiceRecordService: IServiceRecordService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public ServiceRecordService(IServiceBase httpService)
        {
            _baseURL = "ServiceRecord";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<ServiceRecord> GetServiceRecordList(Filter filterdata)
        {
            return await _httpService.PostAsync<ServiceRecord, Filter>($"{_baseURL}/GetServiceRecordList", filterdata);
        }
        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetAPIUrl");
        }
    }
}
