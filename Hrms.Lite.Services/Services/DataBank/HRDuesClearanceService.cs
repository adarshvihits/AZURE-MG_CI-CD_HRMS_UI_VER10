using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class HRDuesClearanceService : IHRDuesClearanceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRDuesClearanceService(IServiceBase httpService)
        {
            _baseURL = "HrDuesClearance";
            _httpService = httpService;
        }
        public async Task<EmployeeDueClearance> GetHrDuesClearanceList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeDueClearance, Filter>($"{_baseURL}/GetHrDuesClearanceList", filter);
        }
        public async Task<EmployeeDueClearance> GetDuesClearance_EmployeeWiseDetails(Guid ResignationGI)
        {
            return await _httpService.GetAsync<EmployeeDueClearance>($"{_baseURL}/GetDuesClearance_EmployeeWiseDetails?ResignationGI={ResignationGI}");
        }
        public async Task<ResponseEntity> Save(EmployeeDueClearance model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeDueClearance>($"{_baseURL}/Save", model);
        }
    }
}
