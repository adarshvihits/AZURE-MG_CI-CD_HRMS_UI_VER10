using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
   public class EmployeeVsShiftAllowanceService : IEmployeeVsShiftAllowanceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeVsShiftAllowanceService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsShiftAllowance";
            _httpService = httpService;
        }

      public async Task<EmployeeVsShiftAllowance> GetEmployeeVsShiftAllowanceList(int ShiftCode, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsShiftAllowance, Filter>($"{_baseURL}/GetList?ShiftCode={ShiftCode}", filter);

        }

        public async Task<DataSet> GetEmployeeVsShiftAllowanceList_Excel(int ShiftCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?ShiftCode={ShiftCode}", filter);

        }

        public async Task<ResponseEntity> Save(EmployeeVsShiftAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsShiftAllowance>($"{_baseURL}/Save", model);

        }
    }
}
