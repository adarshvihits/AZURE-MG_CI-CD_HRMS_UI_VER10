using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class TimeOfficeDashBoardService : ITimeOfficeDashBoardService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TimeOfficeDashBoardService(IServiceBase httpService)
        {
            _baseURL = "TimeOfficeDashBoard";
            _baseURL1 = "TimeOfficeDashBoardNew";
            _httpService = httpService;
        }
     public async Task<TimeOfficeDashBoard> GetDashBoardDetails()
        {
            return await _httpService.GetAsync<TimeOfficeDashBoard>($"{_baseURL}/GetDashBoardDetails");
        }
        public async Task<TimeOfficeDashBoard> GetDashBoardList(string Type)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoard>($"{_baseURL}/GetDashBoardList?Type={Type}");
        }
        public async Task<TimeOfficeDashBoard> GetDashBoardDetailsList(string Type, string SubType)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoard>($"{_baseURL}/GetDashBoardDetailsList?Type={Type}&SubType={SubType}");
        }

       public async Task<TimeOfficeDashBoard> GetDashBoardDiagrams(string Diagram, int? ShiftCode)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoard>($"{_baseURL}/GetDashBoardDiagrams?Diagram={Diagram}&ShiftCode={ShiftCode}");
        }

     public   async Task<LowAttendanceRate> GetLowAttendanceRateDetails()
        {
            return await _httpService.GetAsync<LowAttendanceRate>($"{_baseURL}/GetLowAttendanceRateDetails");
        }
        //----New------
        public async Task<TimeOfficeDashBoardNew> GetNewDashBoardDetails()
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardDetails");
        }
       


        //New_TimeOffice_DashBoard
        //****************************************************








        public async Task<TimeOfficeDashBoardNew> GetDashBoardDetailsList(string Type, string SubType, Filter filterdata)
        {
            return await _httpService.PostAsync<TimeOfficeDashBoardNew, Filter>($"TimeOfficeDashBoardNew/GetDashBoardDetailsList?Type={Type}&SubType={SubType}", filterdata);
        }
        public async Task<TimeOfficeDashBoardNew> GetUnAuthorizedLopList(Guid EmployeeGI)
        { 
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetUnAuthorizedLopList?EmployeeGI={EmployeeGI}");
        }

        //Dashboard
        public async Task<TimeOfficeDashBoardNew> GetDashBoardTileDetails()
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardTileDetails");
        }
        public async Task<TimeOfficeDashBoardNew> GetDashBoardWorkStatusDetails()
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardWorkStatusDetails");
        }
        public async Task<TimeOfficeDashBoardNew> GetDashBoardNotificationDetails(string Type)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardNotificationDetails?Type={Type}");
        }
        public async Task<TimeOfficeDashBoardNew> GetDashBoardDiagrams(string Classification)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardDiagrams?Classification={Classification}");
        }

        public async Task<TimeOfficeDashBoardNew> GetDashBoardDetailsListExcel( string Type, string? SubType, Filter filterdata)
        {
            return await _httpService.PostAsync<TimeOfficeDashBoardNew, Filter>($"{_baseURL1}/GetDashBoardDetailsListExcel?Type={Type}&SubType={SubType}", filterdata);

        }
        public async Task<TimeOfficeDashBoardNew> GetLopList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetLopList?EmployeeGI={EmployeeGI}");
        }
        public async Task<TimeOfficeDashBoardNew> GetDashBoardNotificationDetails_PopUp(int NotificationCode)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"TimeOfficeDashBoardNew/GetDashBoardNotificationDetails_PopUp?NotificationCode={NotificationCode}");
        }

        public async Task<DataSet> GetListExcel(string Type, string SubType, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL1}/GetListExcel?Type={Type}&SubType={SubType}", filterdata);

        }

        public async Task<TimeOfficeDashBoardNew> GetABDateList(int EmployeeCode)
        {
            return await _httpService.GetAsync<TimeOfficeDashBoardNew>($"{_baseURL1}/GetABDateList?EmployeeCode={EmployeeCode}");
        }
    }
}
