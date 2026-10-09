using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
  public  class IncidentReportService: IIncidentReportService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public IncidentReportService(IServiceBase httpService)
        {
            _baseURL = "IncidentReport";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<IncidentReport> GetList(int radiomode)
        {
            return await _httpService.GetAsync<IncidentReport>($"{_baseURL}/List?radiomode={radiomode}");
        }

        public async Task<IncidentReport> GetIncidentReportDetails(Guid EmployeeGI, int IncidentCode)
        {
            return await _httpService.GetAsync<IncidentReport>($"{_baseURL}/GetIncidentReportDetails?EmployeeGI={EmployeeGI}&IncidentCode={IncidentCode}");

        }
    

        public Task<ResponseEntity> Create(IncidentReport input)
        {
            return _httpService.PostAsync<ResponseEntity, IncidentReport>($"{_baseURL}/Create", input);
        }
    }
}
