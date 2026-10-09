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
  public  class ReporteesService: IReporteesService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ReporteesService(IServiceBase httpService)
        {
            _baseURL = "Reportees";
            _httpService = httpService;
        }

        public async Task<DataSet> GetExcelList(int Type, int Level, int Reporter, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetExcelList?Type={Type}&Level={Level}&Reporter={Reporter}", filter);
        }

        public async Task<Reportees> GetReporteesList(int Type,int Level,int Reporter, Filter filter)
        {
            return await _httpService.PostAsync<Reportees, Filter>($"{_baseURL}/GetReporteesList?Type={Type}&Level={Level}&Reporter={Reporter}", filter);
        }

     
    }
}
