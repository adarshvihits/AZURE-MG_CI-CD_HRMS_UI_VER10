using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ISocialWallReportService
    {
        public Task<SocialWallReport> GetSocialWallReportList(int MonthCode);
        public Task<SocialWallReport> GetEmployeeDetails(int SocialWallCode, int MonthCode);
    }
}
