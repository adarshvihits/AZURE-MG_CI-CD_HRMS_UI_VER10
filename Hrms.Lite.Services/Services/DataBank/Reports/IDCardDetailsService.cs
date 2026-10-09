using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
   public  class IDCardDetailsService: IIDCardDetailsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public IDCardDetailsService(IServiceBase httpService)
        {
            _baseURL = "IDCardDetails";
            _httpService = httpService;
        }
        public async Task<IDCardDetails> GetIDCardDetails(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<IDCardDetails, Filter>($"{_baseURL}/GetIDCardDetails?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> GetIDCardDetails_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet,Filter>($"{_baseURL}/GetIDCardDetails_Excel?FromDate={FromDate}&ToDate={ToDate}",filter);
        }
        public async Task<File> DownloadDoc(string EmployeeId)
        {
             return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDocCetfication?EmployeeId={EmployeeId}");
        }
    }
}
