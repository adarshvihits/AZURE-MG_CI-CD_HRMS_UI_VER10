using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class LeaveBalanceAsOnDateService: ILeaveBalanceAsOnDateService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveBalanceAsOnDateService(IServiceBase httpService)
        {
            _baseURL = "LeaveBalanceAsOnDate";
            _httpService = httpService;
        }

        public async Task<LeaveBalanceAsOnDate> GetList(string Date, string Type, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveBalanceAsOnDate, Filter>($"{_baseURL}/GetList?Date={Date}&Type={Type}&Leave={Leave}", filter);
        }

        public async Task<DataSet> LeaveBalanceAsOnDate_Excel(string Date, string Type, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveBalanceAsOnDate_Excel?Date={Date}&Type={Type}&Leave={Leave}", filter);
        }

    }
}
