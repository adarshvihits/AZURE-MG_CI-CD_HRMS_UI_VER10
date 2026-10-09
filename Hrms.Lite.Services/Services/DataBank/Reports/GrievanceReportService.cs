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
   
    
    public class GrievanceReportService: IGrievanceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public GrievanceReportService(IServiceBase httpService)
        {
            _baseURL = "GrievanceReport";
            _httpService = httpService;
        }


        public async Task<Grievance> GetGrievanceReportList(string fromdate, string todate, int category, Filter filter)
        {
            return await _httpService.PostAsync<Grievance, Filter>($"{_baseURL}/GetGrievanceReport?FromDate={fromdate}&ToDate={todate}&Category={category}", filter);
        }

        public async Task<DataSet> GetGrievanceReport_Excel(string FromDate, string ToDate, int Category, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExcelGrievanceReport?FromDate={FromDate}&ToDate={ToDate}&Category={Category}", filter);
        }



    }
}
