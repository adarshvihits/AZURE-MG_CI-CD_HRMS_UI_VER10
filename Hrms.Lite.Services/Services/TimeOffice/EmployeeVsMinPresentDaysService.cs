using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class EmployeeVsMinPresentDaysService : IEmployeeVsMinPresentDaysService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeVsMinPresentDaysService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsMinPresentDays";
            _httpService = httpService;
        }
        public async Task<EmployeeVsMinPresentDays> GetEmployeeVsMinPresentDays(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsMinPresentDays, Filter>($"{_baseURL}/GetEmployeeVsMinPresentDays?TabIndex={TabIndex}", filter);

        }

        public async Task<DataSet> GetEmployeeVsMinPresentDays_Excel(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeVsMinPresentDays_Excel?TabIndex={TabIndex}", filter);

        }

        public async Task<ResponseEntity> SaveEmployeeVsMinPresentDays(Guid EmployeeGI, int PresentDays)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/SaveEmployeeVsMinPresentDays?EmployeeGI={EmployeeGI}&PresentDays={PresentDays}");
        }
    }
}
