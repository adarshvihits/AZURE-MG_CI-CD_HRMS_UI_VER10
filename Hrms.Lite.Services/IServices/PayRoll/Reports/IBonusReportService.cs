using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IBonusReportService
    {
        public Task<DataSet> GetBonusProcessingReport(Filter filter, int BonusGroup, int ProcessMonth, int AmountType);
        public Task<DataSet> GetBonusProcessingReport_Excel(Filter filter, int BonusGroup, int ProcessMonth, int AmountType);
    }
}
