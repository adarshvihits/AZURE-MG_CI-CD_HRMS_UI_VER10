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
    public class BonusProcessingService : IBonusProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public BonusProcessingService(IServiceBase httpService)
        {
            _baseURL = "BonusProcessing";
            _httpService = httpService;
        }
        public async Task<ResponseEntity> BonusProcessing(BonusProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, BonusProcessing>($"{_baseURL}/BonusProcessing",model);

        }

       public async Task<BonusProcessing> GetBonusProcessingDetails(int EmployeeCode, int MonthCode, int BonusGroup)
        {
            return await _httpService.GetAsync<BonusProcessing>($"{_baseURL}/GetBonusProcessingDetails?EmployeeCode={EmployeeCode}&MonthCode={MonthCode}&BonusGroup={BonusGroup}");

        }

        public async Task<BonusProcessing> GetBonusProcessingList(int MonthCode, int BonusGroup, Filter filter)
        {
            return await _httpService.PostAsync<BonusProcessing, Filter>($"{_baseURL}/GetBonusProcessingList?MonthCode={MonthCode}&BonusGroup={BonusGroup}", filter);

        }

        public async Task<DataSet> GetBonusProcessingList_Excel(int MonthCode, int BonusGroup,  Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetBonusProcessingList_Excel?MonthCode={MonthCode}&BonusGroup={BonusGroup}", filter);

        }
    }
}
