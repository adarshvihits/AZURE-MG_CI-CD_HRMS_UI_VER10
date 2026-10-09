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
    public class DutyJoiningRptService: IDutyJoiningRptService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DutyJoiningRptService(IServiceBase httpService)
        {
            _baseURL = "DutyJoiningRpt";
            _httpService = httpService;
        }
        public async Task<DutyJoiningRpt> GetList(Filter filter)
        {
            return await _httpService.PostAsync<DutyJoiningRpt, Filter>($"{_baseURL}/GetList?", filter);
        }
    }
}
