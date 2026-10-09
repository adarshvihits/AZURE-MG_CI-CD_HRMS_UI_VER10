using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS
{
 public class Form16SettingsService: IForm16SettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public Form16SettingsService(IServiceBase httpService)
        {
            _baseURL = "Form16Settings";
            _httpService = httpService;
        }
        public Task<ResponseEntity> SaveForm16Settings(Form16Settings input)
        {
            return _httpService.PostAsync<ResponseEntity, Form16Settings>($"{_baseURL}/SaveForm16Settings", input);

        }
        async Task<Form16Settings> IForm16SettingsService.GetFinYearDetails(int AssessmentYear)
        {
            return await _httpService.GetAsync<Form16Settings>($"{_baseURL}/GetFinYearDetails?AssessmentYear={AssessmentYear}");
        }
    }
}
