using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
    public class Form16Service : IForm16Service
    {
        private readonly string _baseURL;

        private readonly IServiceBase _httpService;
        public Form16Service(IServiceBase httpService)
        {
            _baseURL = "Form16";

            _httpService = httpService;
        }
        public async Task<Form16> GetEmployeeList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<Form16, Filter>($"{_baseURL}/GetEmployeeList?AssessmentYear={AssessmentYear}", filter);
        }
    }
}