using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IRecruitmentTransactionHistoryService
    {
        //*******Passing filter model only for server side pagination to pass start,length,searchvalue**********
        public Task<RecruitmentReports> RecruitmentTransactionHistoryReport(int Tab, string Fromdate, string Todate, int UserCode,int MenuCode,Filter filter);
        public Task<DataSet> RecruitmentTransactionHistoryReport_Excel(int Tab, string Fromdate, string Todate, int UserCode,int MenuCode, Filter filter);
        //******************************************************************************************************
    }
}
