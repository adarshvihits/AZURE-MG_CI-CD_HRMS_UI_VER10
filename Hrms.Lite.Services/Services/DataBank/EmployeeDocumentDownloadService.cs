using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{

    public class EmployeeDocumentDownloadService : IEmployeeDocumentDownloadService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeDocumentDownloadService(IServiceBase httpService)
        {
            _baseURL = "EmployeeDocumentDownload";
           
            _httpService = httpService;
        }

        public async Task<EmployeeDocumentDownload> GetEmployeeList()
        {
            return await _httpService.GetAsync<EmployeeDocumentDownload>($"{_baseURL}/GetEmployeeList");
        }

        public async Task<DataSet> GetEmployeeDocument(int employeeCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetEmployeeDocument?employeeCode="+ employeeCode);
        }

    }
}
