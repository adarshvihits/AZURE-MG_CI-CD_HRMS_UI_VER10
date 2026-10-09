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
   
    public class PTProcessingService: IPTProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PTProcessingService(IServiceBase httpService)
        {
            _baseURL = "PTProcessing";
            _httpService = httpService;
        }

        public async Task<PTProcessing> GetBatchUpdateDetails(int Month, int PTgroup)
        {
            return await _httpService.GetAsync<PTProcessing>($"{_baseURL}/GetPTBatchUpdate_Edit_Details?MonthCode={Month}&&PTGroupCode={PTgroup}");
        }

        public async Task<PTProcessing> GetPTGroupDetails(int currMonth, int PTgroup)
        {
            return await _httpService.GetAsync<PTProcessing>($"{_baseURL}/GetPTGroupDetails?MonthCode={currMonth}&&PTGroupCode={PTgroup}");
        }

        public async Task<PTProcessing> GetPTProcessingList(int currMonth, int PTgroup,Filter filter)
        {
            return await _httpService.PostAsync<PTProcessing,Filter>($"{_baseURL}/GetPTProcessingDetails?MonthCode={currMonth}&&PTGroupCode={PTgroup}",filter);
        }


        public async Task<DataSet> GetPTProcessingDetails_Excel(int MonthCode, int PTGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPTProcessingDetails_Excel?MonthCode={MonthCode}&&PTGroupCode={PTGroupCode}", filter);
        }



        public async Task<ResponseEntity> PTProcessingSave(PTProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, PTProcessing>($"{_baseURL}/PTProcessingSave", model);
        }

    }
}
