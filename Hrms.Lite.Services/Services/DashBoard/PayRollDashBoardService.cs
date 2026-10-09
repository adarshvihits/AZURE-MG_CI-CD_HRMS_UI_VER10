using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class PayRollDashBoardService : IPayRollDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PayRollDashBoardService(IServiceBase httpService)
        {
            _baseURL = "PayrollDashBoard";
            _httpService = httpService;
        }
        public async Task<PayrollDashBoard> GetDashBoardDetails()
        {
            return await _httpService.GetAsync<PayrollDashBoard>($"{_baseURL}/GetDashBoardDetails");
        }

        public async Task<PayrollDashBoard> GetDashBoardList(string Type)
        {
            return await _httpService.GetAsync<PayrollDashBoard>($"{_baseURL}/GetDashBoardList?Type={Type}");
        }
        public async Task<PayrollDashBoard> GetDashBoardDiagrams(string Diagram)
        {
            return await _httpService.GetAsync<PayrollDashBoard>($"{_baseURL}/GetDashBoardDiagrams?Diagram={Diagram}");
        }

       public async Task<PayrollDashBoard> GetPFAndESIDetails(string Type, string SubType)
        {
            return await _httpService.GetAsync<PayrollDashBoard>($"{_baseURL}/GetPFAndESIDetails?Type={Type}&SubType={SubType}");
        }

        //--------New--------------
        public async Task<PayRollDashBoardNew> GetDashBoardDetailsNew()
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetDashBoardDetails");
        }
        public async Task<PayRollDashBoardNew> GetPayrollNotifications()
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetPayrollNotifications");
        }
        public async Task<PayRollDashBoardNew> GetDashBoardNewList(string Type)
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetDashBoardList?Type={Type}");
        }

     
        public async Task<PayRollDashBoardNew> GetDashBoardPopUpList(string Type)
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetDashBoardPopUpList?Type={Type}");
        }
        public async Task<PayRollDashBoardNew> GetDashBoardApplicableCount()
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetDashBoardApplicableCount");
        }
        public async Task<PayRollDashBoardNew> GetApplicableCountDetails(string Type, string SubType)
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetApplicableCountDetails?Type={Type}&SubType={SubType}");
        }
        public async Task<PayRollDashBoardNew> GetPayrollGraphDetails(string Classification)
        {
            return await _httpService.GetAsync<PayRollDashBoardNew>($"PayrollDashBoardNew/GetPayrollGraphDetails?Classification={Classification}");
        }
    }
}
