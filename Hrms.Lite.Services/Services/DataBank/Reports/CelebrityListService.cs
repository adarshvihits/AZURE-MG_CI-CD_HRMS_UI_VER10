using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{

    public class CelebrityListService:ICelebrityListService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CelebrityListService(IServiceBase httpService)
        {
            _baseURL = "CelebrityList";
            _httpService = httpService;
        }

        public async Task<CelebrityList> GetCelebrityList(string FromDate, string ToDate, string TabIndex,Filter filter)
        {
           
         return await _httpService.PostAsync<CelebrityList, Filter>($"{_baseURL}/GetCelebrityList?FromDate={FromDate}&ToDate={ToDate}&TabIndex={TabIndex}", filter);

        }

       public async Task<DataSet> GetCelebrityList_Excel(string FromDate, string ToDate, string TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCelebrityList_Excel?FromDate={FromDate}&ToDate={ToDate}&TabIndex={TabIndex}", filter);

        }
    }
}
