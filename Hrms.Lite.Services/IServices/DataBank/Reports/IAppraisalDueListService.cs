using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IAppraisalDueListService
    {
        public Task<AppraisalDueList> GetAppraisalDueList(int Type,int duration,Filter filter);
        public Task<DataSet> AppraisalDueList_Excel(int Type, int duration, Filter filter);

    }
}
