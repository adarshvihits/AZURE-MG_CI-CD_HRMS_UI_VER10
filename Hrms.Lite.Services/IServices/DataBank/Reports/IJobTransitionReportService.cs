using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IJobTransitionReportService
    {

        public Task<JobTransitionReport> GetJobTransitionReport( string From, string To, int TransitionType, JobTransitionReport model );

        public Task<JobTransitionReport> GetJobTransitionReportDetails(string From, string To, int TransitionType, JobTransitionReport model); //SERVER SIDE PAGING
        public Task<DataSet> GetJobTransitionReportDetails_Excel( string From, string To, int TransitionType, JobTransitionReport model);
        public Task<JobTransitionReport> GetGridSettingDetails();
    }
}
