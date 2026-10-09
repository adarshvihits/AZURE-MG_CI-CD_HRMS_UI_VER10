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
    public class PeriodicalEmployeelistService: IPeriodicalEmployeelistService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PeriodicalEmployeelistService(IServiceBase httpService)
        {
            _baseURL = "PeriodicalEmployeelist";
            _httpService = httpService;
        }




        public async Task<PeriodicalEmployeelist> GetPeriodicalEmployeelistDetails(string FromDate, string ToDate, Filter filter)
        {

            return await _httpService.PostAsync<PeriodicalEmployeelist, Filter>($"{_baseURL}/GetPeriodicalEmployeelistDetails?FromDate={FromDate}&ToDate={ToDate}", filter);

        }


        public async Task<DataSet> PeriodicalEmployeelist_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/PeriodicalEmployeelist_Excel?FromDate={FromDate}&ToDate={ToDate}", filter);

        }

    }
}
