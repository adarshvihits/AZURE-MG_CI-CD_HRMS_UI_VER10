using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
 public   class Form12LabourReportService: IForm12LabourReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public Form12LabourReportService(IServiceBase httpService)
        {
            _baseURL = "Form12LabourReport";
            _httpService = httpService;
        }

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, Form12LabourReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, Form12LabourReport>($"{_baseURL}/GetForm12Report?SalaryPeriodCode={SalaryPeriod}", model);
        }
    }
}
