using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
   public  class LeaveLapsService: ILeaveLapsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveLapsService(IServiceBase httpService)
        {
            _baseURL = "LeaveLaps";
            _httpService = httpService;
        }

        public async Task<LeaveLaps> GetLeaveLapsList(int LeavePeriodCode, int LeaveCode, Filter filter1)
        {
            return await _httpService.PostAsync<LeaveLaps, Filter>($"{_baseURL}/GetLeaveLapsList?LeavePeriod={LeavePeriodCode}&LeaveCode={LeaveCode}", filter1);
        }

        public async Task<DataSet> GetLeaveLapsList_Excel(int LeavePeriodCode, int LeaveCode, Filter filter1)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLeaveLapsList_Excel?LeavePeriod={LeavePeriodCode}&LeaveCode={LeaveCode}", filter1);
        }

        public Task<ResponseEntity> LeaveLapsSave(LeaveLaps model)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveLaps>($"{_baseURL}/LeaveLapsSave", model);
        }

        public Task<ResponseEntity>LeaveLapsBatchUpdate(LeaveLaps model)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveLaps>($"{_baseURL}/LeaveLapsBatchUpdate", model);
        }

    }
}
