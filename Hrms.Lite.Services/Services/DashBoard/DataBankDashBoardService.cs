using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class DataBankDashBoardService : IDataBankDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DataBankDashBoardService(IServiceBase httpService)
        {
            _baseURL = "DataBankDashBoard";
            _httpService = httpService;
        }
        public async Task<DataBankDashBoard> GetDashBoardDetails()
        {
            return await _httpService.GetAsync<DataBankDashBoard>($"{_baseURL}/GetDashBoardDetails");
        }

       public async Task<DataBankDashBoard> GetDashBoardDetailsList(string Type,string SubType)
        {
            return await _httpService.GetAsync<DataBankDashBoard>($"{_baseURL}/GetDashBoardDetailsList?Type={Type}&SubType={SubType}");
        }

      public async Task<DataBankDashBoard> GetDashBoardList(string Type)
        {
            return await _httpService.GetAsync<DataBankDashBoard>($"{_baseURL}/GetDashBoardList?Type={Type}");
        }

     public async Task<DataBankDashBoard> GetDashBoardDiagrams(string Diagram, string ToggleActive, int Classification)
        {
            return await _httpService.GetAsync<DataBankDashBoard>($"{_baseURL}/GetDashBoardDiagrams?Diagram={Diagram}&ToggleActive={ToggleActive}&Classification={Classification}");
        }

      public async Task<DataBankDashBoard> GetDashBoardDiagramsDetails(string Diagram, string ToggleActive, string Type)
        {
            return await _httpService.GetAsync<DataBankDashBoard>($"{_baseURL}/GetDashBoardDiagramsDetails?Diagram={Diagram}&ToggleActive={ToggleActive}&Type={Type}");
        }

        //-----New------
        public async Task<DataBankDashBoardNew> GetNewDashBoardDetails()
        {
            return await _httpService.GetAsync<DataBankDashBoardNew>($"DataBankDashBoardNew/GetDashBoardDetails");
        }
        public async Task<DataBankDashBoardNew> GetDatabankNotifications()
        {
            return await _httpService.GetAsync<DataBankDashBoardNew>($"DataBankDashBoardNew/GetDatabankNotifications");
        }
        public async Task<DataBankDashBoardNew> GetDashBoardDiagrams(string Classification)
        {
            return await _httpService.GetAsync<DataBankDashBoardNew>($"DataBankDashBoardNew/GetDashBoardDiagrams?Classification={Classification}");
        }

        public async Task<DataBankDashBoardNew> GetDashBoardNotificationDetails_PopUp(string Type)
        {
            return await _httpService.GetAsync<DataBankDashBoardNew>($"DataBankDashBoardNew/GetDashBoardNotificationDetails_PopUp?Type={Type}");
        }
        public async Task<DataSet> GetDashBoardNotificationDetailsPopUp_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"DataBankDashBoardNew/GetDashBoardNotificationDetailsPopUp_Excel");
        }

    }
}
