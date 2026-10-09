using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
 public  class TDS24QService: ITDS24QService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDS24QService(IServiceBase httpService)
        {
            _baseURL = "TDS24Q";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails( TDS24Q model)
        {
            return _httpService.PostAsync<System.Data.DataSet, TDS24Q>($"{_baseURL}/GetTDS24QData", model);
        }
    }
}
