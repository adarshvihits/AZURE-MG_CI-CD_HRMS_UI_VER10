using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class UHIDService : IUHIDService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public UHIDService(IServiceBase httpService)
        {
            _baseURL = "UHID";
            _httpService = httpService;
        }

        public async Task<UHID> GetUHIDList(Filter filter)
        {
            return await _httpService.PostAsync<UHID, Filter>($"{_baseURL}/UHIDList", filter);
        }
        public async Task<UHID> GetEmployeeDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<UHID>($"{_baseURL}/GetEmployeeDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> EmployeeWiseUpdate(UHID Input)
        {
            return await _httpService.PostAsync<ResponseEntity, UHID>($"{_baseURL}/EmployeeWiseUpdate", Input);
        }

        public async Task<DataSet> UHIDList_Excel(string Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/UHIDList_Excel?Type={Type}", filter);
        }
        public async Task<UHID> GetUHIDDetails(string Type, Filter filter1)
        {
            return await _httpService.PostAsync<UHID, Filter>($"{_baseURL}/GetUHIDDetails?Type={Type}", filter1);
        }

    }
}
