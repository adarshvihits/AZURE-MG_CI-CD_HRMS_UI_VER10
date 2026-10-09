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
    public class AppraisalDueListService : IAppraisalDueListService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AppraisalDueListService(IServiceBase httpService)
        {
            _baseURL = "AppraisalDueList";
            _httpService = httpService;
        }



        public async Task<AppraisalDueList> GetAppraisalDueList(int Type, int duration, Filter Filter)
        {
            return await _httpService.PostAsync<AppraisalDueList, Filter>($"{_baseURL}/GetAppraisalDueList?Type={Type}&duration={duration}", Filter);
        }


        public async Task<DataSet> AppraisalDueList_Excel(int Type, int duration, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/AppraisalDueList_Excel?Type={Type}&duration={duration}", filter);

        }



    }
}
