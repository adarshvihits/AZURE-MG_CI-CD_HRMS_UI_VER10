using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsBankDetailsApprovalService : IEmployeeVsBankDetailsApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsBankDetailsApprovalService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsBankDetailsApproval";
            _httpService = httpService;
        }
        public async Task<EmployeeVsBankDetailsApproval> GetList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsBankDetailsApproval, Filter>($"{_baseURL}/GetList", filter);
        }
        public async Task<EmployeeVsBankDetailsApproval> GetEmpBankDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeVsBankDetailsApproval>($"{_baseURL}/GetEmpBankDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> ApproveOrReject(Guid EmployeeGI, string mode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ApproveOrReject?EmployeeGI={EmployeeGI}&mode={mode}");
        }
        public async Task<DataSet> GetList_Excel(Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", filterdata);
        }
        public async Task<File> DownloadDoc(Guid RequestGI, string DocType)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?RequestGI={RequestGI}&DocType={DocType}");
        }
    }
}
