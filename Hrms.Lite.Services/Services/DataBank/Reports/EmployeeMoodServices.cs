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
   public  class EmployeeMoodServices : IEmployeeMoodServices
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeMoodServices(IServiceBase httpService)
        {
            _baseURL = "EmployeeMood";
            _httpService = httpService;
        }

        public async Task<EmployeeMood> GetEmployeeMoodDetails(string Date,int moodcode,Filter filter)
        {

            return await _httpService.PostAsync<EmployeeMood,Filter>($"{_baseURL}/GetEmployeeMoodDetails?Date={Date}&moodcode={moodcode}",filter);

        }

        public async Task<DataSet> EmployeeMoodList_Excel(Filter filter, string Date, int moodcode)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeMoodList_Excel?Date={Date}&moodcode={moodcode}",filter);

        }

        public async Task<EmployeeMood> GetApplyFilter(string Date, int moodcode, Filter filter)
        {

            return await _httpService.PostAsync<EmployeeMood, Filter>($"{_baseURL}/GetApplyFilter?Date={Date}&moodcode={moodcode}", filter);

        }
    }
}
