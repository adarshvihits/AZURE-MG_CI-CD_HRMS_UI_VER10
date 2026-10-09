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
    public class ParentsMediclaimEMIService: IParentsMediclaimEMIService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ParentsMediclaimEMIService(IServiceBase httpService)
        {
            _baseURL = "ParentsMediclaimEMI";
            _httpService = httpService;
        }
        public async Task<ParentsMediclaimEMI> GetList(int Month, int Finyear, Filter filter)
        {
            return await _httpService.PostAsync<ParentsMediclaimEMI, Filter>($"{_baseURL}/GetList?Month={Month}&Financialyear={Finyear}", filter);

        }
        public async Task<DataSet> GetList_Excel(int Month, int Finyear, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Month={Month}&Financialyear={Finyear}", filter);

        }
        public async Task<ResponseEntity> Save(int Month, int Finyear, Filter filter)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/Save?Month={Month}&Financialyear={Finyear}", filter);

        }
    }
}
