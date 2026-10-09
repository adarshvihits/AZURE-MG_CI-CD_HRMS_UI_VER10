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
   public class ShiftVarianceReportService : IShiftVarianceReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ShiftVarianceReportService(IServiceBase httpService)
        {
            _baseURL = "ShiftVarianceReport";
            _httpService = httpService;
        }

        public async Task<ShiftVarianceReport> GetShiftVarianceReport(string Date, Filter filter)
        {
            return await _httpService.PostAsync<ShiftVarianceReport, Filter>($"{_baseURL}/GetShiftVarianceReport?Date={Date}", filter);
        }
     
        public async Task<DataSet> GetShiftVarianceReport_Excel(string Date, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetShiftVarianceReport_Excel?Date={Date}", filter);
        }
    }
}
