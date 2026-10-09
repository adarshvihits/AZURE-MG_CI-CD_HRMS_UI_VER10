using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
    public interface ILandingPageService
    {
        public Task<LandingPage> GetDashboardDetails();
        public Task<LandingPage> GetDatabankNotifications(string Type);
        public Task<TimeOfficeDashBoardNew> GetTimeOfficeNotifications();
        public Task<LandingPage> GetPayrollNotifications(string Type);
        Task<PayRollDashBoardNew> GetPayrollApplicableCount();
        Task<PayRollDashBoardNew> GetApplicableCountDetails(string Type, string SubType);
        public Task<LandingPage> GetLoginDetails(string fromDate, string toDate);
        public Task<LandingPage> GetJobTransferPopupDetails();
        public Task<LandingPage> GetPromotionPopupDetails();
    }
}
