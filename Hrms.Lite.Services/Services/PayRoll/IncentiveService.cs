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
  public  class IncentiveService: IIncentiveService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public IncentiveService(IServiceBase httpService)
        {
            _baseURL = "Incentive";
            _httpService = httpService;
        }
        public async Task<Incentive> GetList(int Month, int BonusCategoryCode, Filter filter)
        {
            return await _httpService.PostAsync<Incentive, Filter>($"{_baseURL}/GetIncentiveList?Month={Month}&BonusCategoryCode={BonusCategoryCode}", filter);
        }
        public async Task<ResponseEntity> Save(Incentive model)
        {
            return await _httpService.PostAsync<ResponseEntity, Incentive>($"{_baseURL}/GetIncentiveSave", model);
        }
        public async Task<ResponseEntity<List<Incentive>>> ImportFromExcel(Incentive model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<Incentive>>, Incentive>($"{_baseURL}/ImportFromExcel", model);

        }
        public async Task<DataSet> IncentiveList_Excel(int Month, int BonusCategoryCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/IncentiveList_Excel?Month={Month}&BonusCategoryCode={BonusCategoryCode}", Filter);
        }


    }
}
