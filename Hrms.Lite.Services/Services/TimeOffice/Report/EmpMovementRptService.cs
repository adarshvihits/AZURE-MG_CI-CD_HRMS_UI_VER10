using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class EmpMovementRptService: IEmpMovementRptService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmpMovementRptService(IServiceBase httpService)
        {
            _baseURL = "EmpMovementRpt";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails(EmployeeMovementReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, EmployeeMovementReport>($"{_baseURL}/GetDetails", model);
        }
    }
}
