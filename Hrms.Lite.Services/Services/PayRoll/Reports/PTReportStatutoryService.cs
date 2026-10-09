using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
   public class PTReportStatutoryService : IPTReportStatutoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public PTReportStatutoryService(IServiceBase httpService)
        {
            _baseURL = "PTReportStatutory";
            _baseURL1 = "PTReportStatutoryDetails";
            _httpService = httpService;
        }

        public async Task<PTReportStatutory> GetList(int Month, Filter filter)
        {
            return await _httpService.PostAsync<PTReportStatutory, Filter > ($"{_baseURL}/GetReportList?Month={Month}",filter);
        }



        public async Task<PTReportStatutory> GetPTReportStatutory(int Month)
        {
            return await _httpService.GetAsync<PTReportStatutory>($"{_baseURL1}/GetPTReportStatutoryPrint?Month={Month}");
        }



    }
}
