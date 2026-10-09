using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IVacancyStatusReportService
    {
        public Task<RecruitmentReports> GetVacancyStatusReport(Filter filter);
        public Task<DataSet> GetVacancyStatusReport_Excel(Filter filter);
    }
}
