using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
    public interface IDataBankDashBoardService
    {
        public Task<DataBankDashBoard> GetDashBoardDetails();
        public Task<DataBankDashBoard> GetDashBoardDetailsList(string Type,string SubType);
        public Task<DataBankDashBoard> GetDashBoardList( string Type);
        public Task<DataBankDashBoard> GetDashBoardDiagrams(string Diagram, string ToggleActive,int  Classification);
        public Task<DataBankDashBoard> GetDashBoardDiagramsDetails( string Diagram, string ToggleActive, string Type);
       

        //-----New---------
        public Task<DataBankDashBoardNew> GetNewDashBoardDetails();
        public Task<DataBankDashBoardNew> GetDatabankNotifications();
        Task<DataBankDashBoardNew> GetDashBoardNotificationDetails_PopUp(string Type);
        Task<DataBankDashBoardNew> GetDashBoardDiagrams(string Classification);
        public Task<DataSet> GetDashBoardNotificationDetailsPopUp_Excel();
    }
}
