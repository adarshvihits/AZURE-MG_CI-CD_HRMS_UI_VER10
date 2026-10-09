using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
   public interface ITDSSlipService
    {
        public Task<TDSSlip> GetMonths(int AssessmentYearCode);
        public Task<TDSSlip> GetEmployeeList(int SalaryPeriodCode, Filter filter);
        public Task<Url> GetUrl();
        public Task<Url> GetTDSSlip();
    }
}
