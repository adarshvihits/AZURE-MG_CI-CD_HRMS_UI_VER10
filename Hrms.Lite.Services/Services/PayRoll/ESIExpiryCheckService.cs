using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.PayRoll
{
    public class ESIExpiryCheckService :IESIExpiryCheckService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;


        public ESIExpiryCheckService(IServiceBase httpService)
        {
            _baseURL = "ESIExpiryCheck";
            _httpService = httpService;
        }

         public async Task<ESIExpiryCheck> GetESIExpiryCheckEmployeeList(int Month, Filter filter)
        {
            return await _httpService.PostAsync<ESIExpiryCheck, Filter>($"{_baseURL}/GetESIExpiryCheckEmployeeList?Month={Month}", filter);
        }
        public async Task<DataSet> GetESIExpiryCheckList_Excel(int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetESIExpiryCheckList_Excel?Month={Month}", filter);
        }
        async Task<ResponseEntity> IESIExpiryCheckService.SaveESIApplicabilty(ESIExpiryCheck Model)
        {
            return await _httpService.PostAsync<ResponseEntity, ESIExpiryCheck>($"{_baseURL}/SaveESIExpirtExitDetails", Model);
        }

    }
}
