using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IParentsMediclaimRequestReportService
    {
        public Task<ParentsMediclaimRequestReport> GetList(int FinancialYearcode, Filter filterdata);
        public Task<DataSet> GetList_Excel(int FinancialYearcode, Filter Filter);

        
    }
}


