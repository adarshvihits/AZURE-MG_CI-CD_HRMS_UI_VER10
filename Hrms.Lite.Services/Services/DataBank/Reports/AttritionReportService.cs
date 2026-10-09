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
    public class AttritionReportService: IAttritionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public AttritionReportService(IServiceBase httpService)
        {
            _baseURL = "AttritionReport";
            _httpService = httpService;
        }
        public async Task<AttritionReport> GetAttritionReport(AttritionReport model)
        {
            return await _httpService.PostAsync<AttritionReport, AttritionReport>($"{_baseURL}/GetAttritionReport", model);
        }
    }
}
