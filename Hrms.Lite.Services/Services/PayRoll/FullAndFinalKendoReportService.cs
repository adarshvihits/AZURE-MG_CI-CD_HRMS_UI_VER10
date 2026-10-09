using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class FullAndFinalKendoReportService : IFullAndFinalKendoReportService
    {
        private readonly string _baseURL;

        private readonly IServiceBase _httpService;

        public FullAndFinalKendoReportService(IServiceBase httpService)
        {
            _baseURL = "Common";

            _httpService = httpService;
        }

        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetAPIUrl");
        }
    }
}
