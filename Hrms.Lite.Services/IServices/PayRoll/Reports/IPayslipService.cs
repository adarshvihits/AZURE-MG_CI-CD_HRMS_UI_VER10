using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPayslipService
    {
        public Task<PaySlip> GetMonths(int FinancialYearCode);
        public Task<PaySlip> GetEmployeeList(int SalaryPeriodCode, Filter filter);
        public Task<Url> GetUrl();
        public Task<Url> GetPaySlip();
    }
}
