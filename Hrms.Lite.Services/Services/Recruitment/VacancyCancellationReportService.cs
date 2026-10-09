using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
  public  class VacancyCancellationReportService: IVacancyCancellationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public VacancyCancellationReportService(IServiceBase httpService)
        {
            _baseURL = "VacancyCancellation";
            _httpService = httpService;
        }

        public async Task<VacancyCancellation> GetList()
        {
            return await _httpService.GetAsync<VacancyCancellation>($"{_baseURL}/GetVacancyCancellationReport");
        }
    }
}
