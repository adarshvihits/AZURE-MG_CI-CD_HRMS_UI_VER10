using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IGrievanceReportService
    {
        public Task<Grievance> GetGrievanceReportList(string fromdate,string todate,int category,Filter filter);
        Task<DataSet> GetGrievanceReport_Excel(string FromDate, string ToDate,int Category, Filter filter);
    }
}
