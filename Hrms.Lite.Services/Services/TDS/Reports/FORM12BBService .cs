using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
   public class FORM12BBService : IFORM12BBService
    {
        private readonly string _baseURL;
     
        private readonly IServiceBase _httpService;
        public FORM12BBService(IServiceBase httpService)
        {
            _baseURL = "Form12BB";
       
            _httpService = httpService;
        }
        public async Task<Form12B> GetEmployeeList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<Form12B, Filter>($"{_baseURL}/GetEmployeeList?AssessmentYear={AssessmentYear}", filter);
        }
    }
}
