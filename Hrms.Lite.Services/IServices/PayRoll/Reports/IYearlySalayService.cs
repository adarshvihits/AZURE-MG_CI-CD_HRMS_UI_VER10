using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IYearlySalayService
    {
        public Task<YearlySalary> GetYearlySalaryDetails(YearlySalary model);
    }
}
