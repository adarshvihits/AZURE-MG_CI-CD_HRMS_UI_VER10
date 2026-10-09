using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
    public interface ITimeOfficeDashBoardService
    {
        Task<TimeOfficeDashBoard> GetDashBoardDetails();
        Task<TimeOfficeDashBoard> GetDashBoardList(string Type);
        Task<TimeOfficeDashBoard> GetDashBoardDetailsList(string Type, string SubType);
        Task<TimeOfficeDashBoard> GetDashBoardDiagrams(string Diagram, int? ShiftCode);
        Task<LowAttendanceRate> GetLowAttendanceRateDetails();
        //Task<TimeOfficeDashBoard> GetMaternityESILeaveCompletionList();

        //----New------
        public Task<TimeOfficeDashBoardNew> GetNewDashBoardDetails();




        //New_TimeOffice_DashBoard
        //****************************************************
        Task<TimeOfficeDashBoardNew> GetDashBoardDetailsList(string Type, string SubType, Filter filterdata);
        Task<TimeOfficeDashBoardNew> GetUnAuthorizedLopList(Guid EmployeeGI);

        //Dashboard
        Task<TimeOfficeDashBoardNew> GetDashBoardNotificationDetails(string Type);
        Task<TimeOfficeDashBoardNew> GetDashBoardWorkStatusDetails();
        Task<TimeOfficeDashBoardNew> GetDashBoardTileDetails();
        Task<TimeOfficeDashBoardNew> GetDashBoardDiagrams(string Classification);



        Task<TimeOfficeDashBoardNew> GetDashBoardDetailsListExcel(string Type, string? SubType, Filter filterdata);

        Task<TimeOfficeDashBoardNew> GetLopList(Guid EmployeeGI);
        Task<TimeOfficeDashBoardNew> GetABDateList(int EmployeeCode);
        Task<TimeOfficeDashBoardNew> GetDashBoardNotificationDetails_PopUp(int NotificationCode);

        Task<DataSet> GetListExcel(string Type, string SubType, Filter filterdata);
    }
}

        
