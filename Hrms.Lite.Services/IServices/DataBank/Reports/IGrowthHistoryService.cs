using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IGrowthHistoryService
    {
        public Task<EmployeeProfile> GetEmployeeList(int Tabindex, Filter filterdata);

        public Task<System.Data.DataSet> GetEmployeeList_Excel( int TabIndex, Filter filterdata);
        public Task<EmployeeProfile> GetGrowthHistory(Guid EmployeeGI);
        public Task<JobTransition> GetGrowthHistoryDetails(Guid EmployeeGI, Guid HistoryGI);
    }
}
