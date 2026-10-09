using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IVacancyStatusReportService
    {
        public Task<RecruitmentReports> GetOnboardingVacancyStatusReport();
        public Task<DataSet> GetOnboardingVacancyStatusReport_Excel();
    }
}
