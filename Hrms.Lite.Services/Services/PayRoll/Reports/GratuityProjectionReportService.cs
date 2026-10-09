using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class GratuityProjectionReportService: IGratuityProjectionReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public GratuityProjectionReportService(IServiceBase httpService)
        {
            _baseURL = "GratuitySettings";
            _httpService = httpService;
        }


        public async Task<GratuitySettings> GratuityProjectionList(int Type, Filter filter)
        {
            return await _httpService.PostAsync<GratuitySettings, Filter>($"{_baseURL}/GetGratuityProjection?Type={Type}", filter);
        }

      public  async Task<DataSet> GetGratuityProjection_Excel(int Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetGratuityProjection_Excel?Type={Type}", filter);
        }
    }
}
