using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.DataBank.Reports
{

    public class OrganizationalChartService: IOrganizationalChartService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OrganizationalChartService(IServiceBase httpService)
        {
            _baseURL = "OrganizationalChart";
            _httpService = httpService;
        }

        public async Task<OrganizationalChart> GetOrgChart(int empCode)
        {
            return await _httpService.GetAsync<OrganizationalChart>($"{_baseURL}/GetOrgChart?empCode={empCode}");
        }

        public async Task<OrganizationalChart> GetEmployeeList()
        {
            return await _httpService.GetAsync<OrganizationalChart>($"{_baseURL}/GetEmployeeList");
        }

    }
}
