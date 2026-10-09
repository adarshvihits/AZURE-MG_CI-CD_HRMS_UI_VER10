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
    public class EmployeeBGVService: IEmployeeBGVService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeBGVService(IServiceBase httpService)
        {
            _baseURL = "EmployeeBGV";
            _httpService = httpService;
        }
        public async Task<EmployeeBGV> GetEmployeeList(Filter filter, int TabIndex)
        {
            return await _httpService.PostAsync<EmployeeBGV, Filter>($"{_baseURL}/GetEmployeeList?TabIndex={TabIndex}", filter);
        }
        public async Task<EmployeeBGV> GetApplicantDetails(Guid EmployeeGI,int ApplicantCode)
        {
            return await _httpService.GetAsync<EmployeeBGV>($"{_baseURL}/GetApplicantDetails?EmployeeGI={EmployeeGI}&ApplicantCode={ApplicantCode}");
        }
        public async Task<ResponseEntity> SaveBGV(EmployeeBGV model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeBGV>($"{_baseURL}/SaveBGV", model);
        }
        public async Task<DataSet> GetEmployeeList_Excel(Filter filter, int TabIndex)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel?TabIndex={TabIndex}", filter);
        }
    }
}
