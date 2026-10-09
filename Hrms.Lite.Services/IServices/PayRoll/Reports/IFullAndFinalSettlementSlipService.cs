using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IFullAndFinalSettlementSlipService
    {
        public Task<FullandFinalSettelment> GetList(string fromdate, string todate, Filter filter);

        public Task<DataSet> GetFullandFinalReport_Excel( string FromDate, string ToDate, Filter filter);
        public Task<FullandFinalSettelment> GetEmployeeDetails(Guid EmployeeGI,Guid FinalSettlementGI, string Mode);


        public Task<FullandFinalSettelment> FandFDetailedReport(int MonthFRom, int MonthTo,Filter filter);
    }
}
