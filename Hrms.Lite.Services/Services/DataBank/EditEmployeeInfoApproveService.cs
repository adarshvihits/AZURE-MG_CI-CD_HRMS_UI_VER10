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
    public class EditEmployeeInfoApproveService : IEditEmployeeInfoApproveService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EditEmployeeInfoApproveService(IServiceBase httpService)
        {
            _baseURL = "EditEmployeeInfoApprove";
            _httpService = httpService;
        }

        public async Task<EditEmployeeInfoApprove> GetEditEmployeeInfoApproveDetails(int searchID, Filter filter )
        {
            return await _httpService.PostAsync<EditEmployeeInfoApprove, Filter>($"{_baseURL}/GetEditEmployeeInfoApproveDetails?searchID={searchID}", filter);
        }

        async Task<EditEmployeeInfoApprove> IEditEmployeeInfoApproveService.GetEditEmployeeApproveDetails(Guid EmployeeGI, Guid LogGI)
        {
            return await _httpService.GetAsync<EditEmployeeInfoApprove>($"{_baseURL}/GetEditEmployeeApproveDetails?EmployeeGI={EmployeeGI}&LogGI={LogGI}");
        }

        async Task<ResponseEntity> IEditEmployeeInfoApproveService.ApproveEditApproveEmployeeInfo(EditEmployeeInfoApprove model)
        {
            return await _httpService.PostAsync<ResponseEntity, EditEmployeeInfoApprove>($"{_baseURL}/ApproveEditApproveEmployeeInfo", model);
        }

        public async Task<DataSet> EditEmployeeInfoApprove_Excel( Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EditEmployeeInfoApprove_Excel", filter);
        }
    }
}
