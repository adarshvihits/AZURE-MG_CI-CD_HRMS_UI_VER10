using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class PaySheetService:IPaySheetService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PaySheetService(IServiceBase httpService)
        {
            _baseURL = "PaySheet";
            _httpService = httpService;
        }

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod,PaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, PaySheet>($"{_baseURL}/GetPaySheet?SalaryPeriodCode={SalaryPeriod}", model);
        }

        public Task<System.Data.DataSet> GetPaySheetDraftDetails(int SalaryPeriod, PaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, PaySheet>($"{_baseURL}/GetPaySheetDraft?SalaryPeriodCode={SalaryPeriod}", model);
        }
        public Task<System.Data.DataSet> GetPaySheetPeriodWiseDetails(int SalaryPeriodFROM, int SalaryPeriodTO, PaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, PaySheet>($"{_baseURL}/GetPaySheetPeriodWise?SalaryPeriodCodeFROM={SalaryPeriodFROM}&SalaryPeriodCodeTO={SalaryPeriodTO}", model);
        }
        public async Task<int> GetPaysheetDataCount(int Month, PaySheet model, string Type)
        {
            return await _httpService.PostAsync<int, PaySheet>($"{_baseURL}/GetPaysheetDataCount?Month={Month}&Type={Type}", model);
        }
    }
}
