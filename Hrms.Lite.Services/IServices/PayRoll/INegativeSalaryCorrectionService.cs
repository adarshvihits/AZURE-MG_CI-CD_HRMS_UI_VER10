using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface INegativeSalaryCorrectionService
    {
        public Task<NegativeSalary> GetList(int SalaryPeriod, int payrollgroup, Filter filter);
    }
}
