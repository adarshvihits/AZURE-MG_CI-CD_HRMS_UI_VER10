using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
  public  class TDSVariationReportService : ITDSVariationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSVariationReportService(IServiceBase httpService)
        {
            _baseURL = "TDSVariationReportS";
            _httpService = httpService;
        }
        
        public async Task<ResponseEntity> TdsVariationReport(TDSVariationReport Model)
        {
            return await _httpService.PostAsync<ResponseEntity, TDSVariationReport>($"{_baseURL}/TDSProcessing", Model);
        }
       
    }
}
