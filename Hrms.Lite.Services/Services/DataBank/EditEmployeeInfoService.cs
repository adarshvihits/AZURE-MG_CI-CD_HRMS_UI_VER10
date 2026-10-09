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
    public class EditEmployeeInfoService : IEditEmployeeInfoService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EditEmployeeInfoService(IServiceBase httpService)
        {
            _baseURL = "EditEmployeeInfo";
            _httpService = httpService;
        }
        async Task<EditEmployeeInfo> IEditEmployeeInfoService.GetEditEmployeeInfoList(int Tabindex, int SearchId,Filter filter)
        {
            return await _httpService.PostAsync<EditEmployeeInfo,Filter>($"{_baseURL}/GetEditEmployeeInfoList?Tabindex={Tabindex}&SearchId={SearchId}",filter);
        }
        async Task<EditEmployeeInfo> IEditEmployeeInfoService.GetEditEmployeeInfoDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EditEmployeeInfo>($"{_baseURL}/GetEditEmployeeInfoDetails?EmployeeGI={EmployeeGI}");
        }
        async Task<EditEmployeeInfo> IEditEmployeeInfoService.GetEditEmployeeInfoLogDetails(Guid EmployeeGI, Guid LogGI)
        {
            return await _httpService.GetAsync<EditEmployeeInfo>($"{_baseURL}/GetEditEmployeeInfoLogDetails?EmployeeGI={EmployeeGI}&LogGI={LogGI}");
        }
        async Task<EditEmployeeInfo> IEditEmployeeInfoService.GetEditEmployeeInfoApproveDetails(Guid EmployeeGI, Guid LogGI)
        {
            return await _httpService.GetAsync<EditEmployeeInfo>($"{_baseURL}/GetEditEmployeeInfoApproveDetails?EmployeeGI={EmployeeGI}&LogGI={LogGI}");
        }
        async Task<ResponseEntity> IEditEmployeeInfoService.SaveEditEmployeeInfo(EditEmployeeInfo model)
        {
            return await _httpService.PostAsync<ResponseEntity, EditEmployeeInfo>($"{_baseURL}/SaveEditEmployeeInfo", model);
        }
        async Task<ResponseEntity> IEditEmployeeInfoService.ApproveEditEmployeeInfo(EditEmployeeInfo model)
        {
            return await _httpService.PostAsync<ResponseEntity, EditEmployeeInfo>($"{_baseURL}/ApproveEditEmployeeInfo", model);
        }
        public async Task<EditEmployeeInfo> GetEmployeelistDetails(string Type, Filter filter1)
        {
            return await _httpService.PostAsync<EditEmployeeInfo, Filter>($"{_baseURL}/GetEmployeelistDetails?Type={Type}", filter1);
        }

       

        public async Task<DataSet> editEmployeeInfoTrnList_Excel(Filter filter, int Tabindex, int SearchId)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GeteditEmployeeInfoTrnList_Excel?Tabindex={Tabindex}&SearchId={SearchId}", filter);
        }
    }
}
