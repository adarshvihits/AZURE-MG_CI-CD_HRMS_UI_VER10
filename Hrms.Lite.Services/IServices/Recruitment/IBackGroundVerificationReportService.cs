using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IBackGroundVerificationReportService
    {
        public Task<BackGroundVerificationReport> GetBackGroundVerificationReport(Filter Filter);
        public Task<DataSet> GetBackGroundVerificationReport_Excel(Filter Filter);
    }
}
