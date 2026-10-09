using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IResignationTrackerReportService
    {
        public Task<ResignationTracker> GetResignationTrackingList(Filter filter, string status);
        public Task<DataSet> GetResignationTrackingList_Excel(Filter filter,string status);
        public Task<ResignationTracker> GetResignationTracker(Guid ResignationGI);
        public Task<ResignationTracker> GetTrackerDetails(Guid ResignationGI, Guid EmployeeGI);
        public Task<ResignationTracker> GetWithdrawTrackerDetails(Guid ResignationGI, Guid EmployeeGI);
    }
}
