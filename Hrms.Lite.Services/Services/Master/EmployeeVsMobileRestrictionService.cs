using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class EmployeeVsMobileRestrictionService: IEmployeeVsMobileRestrictionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeVsMobileRestrictionService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsMobileRestriction";
            _httpService = httpService;
        }
        public async Task<EmployeeVsMobileRestriction> GetList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsMobileRestriction, Filter>($"{_baseURL}/GetEmpList?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> RestrictedEmpSave(EmployeeVsMobileRestriction model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsMobileRestriction>($"{_baseURL}/RestrictedEmpSave", model);
        }
        public async Task<DataSet> GetRestrictedEmp_Excel(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetRestrictedEmp_Excel?TabIndex={TabIndex}", filter);
        }
    }
}
