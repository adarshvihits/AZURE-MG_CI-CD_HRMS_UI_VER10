using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class EmpVsMobileRegistrationService : IEmpVsMobileRegistrationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public EmpVsMobileRegistrationService(IServiceBase httpService)
        {
            _baseURL = "EmpVsMobileRegistration";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<EmpVsMobileRegistration> GetList(Filter filter)
        {
            return await _httpService.PostAsync<EmpVsMobileRegistration, Filter>($"{_baseURL}/List?", filter);
        }

        public async Task<ResponseEntity> Delete(EmpVsMobileRegistration model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmpVsMobileRegistration>($"{_baseURL}/Delete", model);

        }
        public async Task<DataSet> EmployeeVsMobileDevice_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeVsMobileDevice_Excel", filter);

        }
    }
}
