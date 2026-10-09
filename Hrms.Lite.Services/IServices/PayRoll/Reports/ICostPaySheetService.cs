using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public  interface ICostPaySheetService
    {

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod,int? CostCenterCode,CostPaySheet model);
        public Task<System.Data.DataSet> GetPaySheetDraftDetails(int SalaryPeriod, CostPaySheet model);

    }
}
