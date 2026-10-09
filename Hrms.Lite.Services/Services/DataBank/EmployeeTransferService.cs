using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeTransferService: IEmployeeTransferService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeTransferService(IServiceBase httpService)
        {
            _baseURL = "EmployeeTransfer";
            _httpService = httpService;
        }
        public async Task<EmployeeTransfer> GetList()
        {
            return await _httpService.GetAsync<EmployeeTransfer>($"{_baseURL}/GetList");
        }
        public async Task<DataSet> GetList_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel");
        }
    }
}
