using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.DataBank
{
  public  class EmployeeVsWorkLocationService : IEmployeeVsWorkLocationService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsWorkLocationService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsWorkLocation";
            _httpService = httpService;
        }

        public async Task<EmployeeVsWorkLocation> GetEmployeeVsWorkLocationList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsWorkLocation, Filter>($"{_baseURL}/GetEmployeeVsWorkLocationList", filter);
        }
        public async Task<ResponseEntity> SaveEmployeeVsWorkLocation(EmployeeVsWorkLocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsWorkLocation>($"{_baseURL}/SaveEmployeeVsWorkLocation", model);
        }

        public async Task<DataSet> GetEmployeeVsWorkLocationList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeVsWorkLocationList_Excel", filter);
        }

    }
}
