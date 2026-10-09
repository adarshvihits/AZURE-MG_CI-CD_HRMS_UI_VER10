using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class LandingPageService: ILandingPageService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly string _baseURL2;
        private readonly IServiceBase _httpService;
        public LandingPageService(IServiceBase httpService)
        {
            _baseURL = "LandingPage";
            _baseURL1 = "TimeOfficeDashBoardNew";
            _baseURL2 = "PayrollDashBoardNew"; 
             _httpService = httpService;
        }
        public async Task<LandingPage> GetDashboardDetails()
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetDashboardDetails");
        }
        public async Task<LandingPage> GetDatabankNotifications(string Type)
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetDatabankNotifications?Type={Type}");
        }
        public async Task<TimeOfficeDashBoardNew> GetTimeOfficeNotifications()
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"{_baseURL1}/GetDashBoardNotificationDetails");
        }
        public async Task<LandingPage> GetPayrollNotifications(string Type)
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetPayrollNotifications?Type={Type}");
        }
        public async Task<PayRollDashBoardNew> GetPayrollApplicableCount()
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"{_baseURL2}/GetDashBoardApplicableCount");
        }
        public async Task<PayRollDashBoardNew> GetApplicableCountDetails(string Type, string SubType)
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"{_baseURL2}/GetApplicableCountDetails?Type={Type}&SubType={SubType}");
        }
        public async Task<LandingPage> GetLoginDetails(string fromDate, string toDate)
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetLoginDetails?fromDate={fromDate}&toDate={toDate}");
        }
        public async Task<LandingPage> GetJobTransferPopupDetails()
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetJobTransferPopupDetails");
        }
        public async Task<LandingPage> GetPromotionPopupDetails()
        {
            return await _httpService.GetAsync<LandingPage>($"{_baseURL}/GetPromotionPopupDetails");
        }
    }
}
