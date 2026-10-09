using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsReportingManagerService : IEmployeeVsReportingManagerService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeVsReportingManagerService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsReportingManager";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<EmployeeVsReportingManager> GetEmployeeVsReportingManagerDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeVsReportingManager>($"{_baseURL}/GetEmployeeVsReportingManagerDetails?EmployeeGI={EmployeeGI}");
        }

        async Task<EmployeeVsReportingManager> IEmployeeVsReportingManagerService.GetList(int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeVsReportingManager, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filterdata);
        }



        public Task<ResponseEntity> Create(EmployeeVsReportingManager input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeVsReportingManager>($"{_baseURL}/Create", input);
        }


    

        public async Task<DataSet> GetList_Excel(int Tabindex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}", Filter);
        }
    }
}
