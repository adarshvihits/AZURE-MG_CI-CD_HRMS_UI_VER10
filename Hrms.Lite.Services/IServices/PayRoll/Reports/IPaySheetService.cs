using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
   public  interface IPaySheetService
    {
        public Task<int> GetPaysheetDataCount(int Month, PaySheet model, string Type);
        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod,PaySheet model);
        public Task<System.Data.DataSet> GetPaySheetDraftDetails(int SalaryPeriod, PaySheet model);
        public Task<System.Data.DataSet> GetPaySheetPeriodWiseDetails(int SalaryPeriodFROM, int SalaryPeriodTO, PaySheet model);

    }
}
