using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IAbsenteeismReportService
    {
        public Task<AbsenteeismReport> GetAbsenteeismReportList(string fromDate, string Todate,int Type,Filter filter);

        public Task<DataSet> AbsenteeismReport_Excel( string FromDate, string ToDate, int Type, Filter filter);
        public Task<AbsenteeismReport> DateWiseStatusPopUp(string date,Filter filter);
        public Task<AbsenteeismReport> EmpWiseStatusPopUp(Guid EmployeeGI, string fromDate, string Todate,Filter filter);
        public Task<AbsenteeismReport> DateWiseReasonPopUp(string date,int column, Filter filter);
        public Task<AbsenteeismReport> EmpWiseReasonPopUp(Guid EmployeeGI, string fromDate, string Todate,int column, Filter filter);
    }
}
