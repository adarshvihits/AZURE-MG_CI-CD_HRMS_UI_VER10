using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
    public interface IPayRollDashBoardService
    {
        Task<PayrollDashBoard> GetDashBoardDetails();
        Task<PayrollDashBoard> GetDashBoardList(string Type);
        Task<PayrollDashBoard> GetDashBoardDiagrams(string Diagram);
        Task<PayrollDashBoard> GetPFAndESIDetails(string Type, string SubType);
        //-------New--------
        Task<PayRollDashBoardNew> GetDashBoardDetailsNew();
        Task<PayRollDashBoardNew> GetPayrollNotifications();
        Task<PayRollDashBoardNew> GetDashBoardNewList(string Type);
        Task<PayRollDashBoardNew> GetDashBoardPopUpList(string Type);
        Task<PayRollDashBoardNew> GetDashBoardApplicableCount();
        Task<PayRollDashBoardNew> GetApplicableCountDetails(string Type, string SubType);
        Task<PayRollDashBoardNew> GetPayrollGraphDetails(string Classification);

    }
}
