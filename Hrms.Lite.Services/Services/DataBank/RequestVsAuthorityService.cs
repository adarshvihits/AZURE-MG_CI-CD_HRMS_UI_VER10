using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class RequestVsAuthorityService: IRequestVsAuthorityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RequestVsAuthorityService(IServiceBase httpService)
        {
            _baseURL = "RequestVsAuthority";
            _httpService = httpService;
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.GetRequestVsAuthorityList(int Tabindex, int SearchId,Filter filter, int RequestCode, int LEVEL)
        {
            return await _httpService.PostAsync<RequestVsAuthority,Filter>($"{_baseURL}/GetRequestVsAuthorityList?Tabindex={Tabindex}&SearchId={SearchId}&RequestCode={RequestCode}&LEVEL={LEVEL}",filter);
        }




        public async Task<DataSet> GetRequestVsAuthorityList_Excel(int Tabindex, int SearchId, int RequestCode, int LEVEL, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetRequestVsAuthorityList_Excel?Tabindex={Tabindex}&SearchId={SearchId}&RequestCode={RequestCode}&LEVEL={LEVEL}", filter);
        }
        async Task<ResponseEntity> IRequestVsAuthorityService.RequestVsAuthorityBatchUpdate(RequestVsAuthority RequestVsAuthority, int Tabindex, int SearchId)
        {
            return await _httpService.PostAsync<ResponseEntity, RequestVsAuthority>($"{_baseURL}/RequestVsAuthorityBatchUpdate?Tabindex={Tabindex}&SearchId={SearchId}", RequestVsAuthority);
        }
        async Task<ResponseEntity> IRequestVsAuthorityService.RequestVsAuthorityBatchUpdateValidation(RequestVsAuthority RequestVsAuthority, int Tabindex, int SearchId)
        {
            return await _httpService.PostAsync<ResponseEntity, RequestVsAuthority>($"{_baseURL}/RequestVsAuthorityBatchUpdateValidation?Tabindex={Tabindex}&SearchId={SearchId}", RequestVsAuthority);
        }
        async Task<ResponseEntity> IRequestVsAuthorityService.RequestVsAuthoritySave_Delete(RequestVsAuthority RequestVsAuthority)
        {
            return await _httpService.PostAsync<ResponseEntity, RequestVsAuthority>($"{_baseURL}/RequestVsAuthoritySave_Delete", RequestVsAuthority);
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.GetRequestVsAuthorityLevelList(Guid EmployeeGI, int code)
        {
            return await _httpService.GetAsync<RequestVsAuthority>($"{_baseURL}/GetRequestVsAuthorityDetails?EmployeeGI={EmployeeGI}&RequestCode={code}");
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.GetRequestVsAuthorityLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<RequestVsAuthority>($"{_baseURL}/GetRequestVsAuthorityLog?EmployeeGI={EmployeeGI}");
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.GetRequestVsAuthorityDetailsAddNew(Guid EmployeeGI, int code)
        {
            return await _httpService.GetAsync<RequestVsAuthority>($"{_baseURL}/GetRequestVsAuthorityDetailsAddNew?EmployeeGI={EmployeeGI}&RequestCode={code}");
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.GetRequestVsAuthorityLevelDetails(Guid EmployeeGI, Guid ReqVsAuthGI)
        {
            return await _httpService.GetAsync<RequestVsAuthority>($"{_baseURL}/GetRequestVsAuthorityDetailsModify?EmployeeGI={EmployeeGI}&RequestGI={ReqVsAuthGI}");
        }
        async Task<RequestVsAuthority> IRequestVsAuthorityService.AlreadyExistCheck(RequestVsAuthority RequestVsAuthority)
        {
            return await _httpService.PostAsync<RequestVsAuthority, RequestVsAuthority>($"{_baseURL}/AlreadyExistCheck", RequestVsAuthority);
        }
        public async Task<ResponseEntity<List<RequestVsAuthority>>> ImportFromExcel(RequestVsAuthority model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<RequestVsAuthority>>, RequestVsAuthority>($"{_baseURL}/ImportFromExcel", model);

        }

       public async Task<ResponseEntity> EmployeeVSChangeAuthorityBatchUpdate(Reportees model)
        {
            return await _httpService.PostAsync<ResponseEntity, Reportees>($"{_baseURL}/EmployeeVSChangeAuthorityBatchUpdate", model);

        }

     
    }
}
