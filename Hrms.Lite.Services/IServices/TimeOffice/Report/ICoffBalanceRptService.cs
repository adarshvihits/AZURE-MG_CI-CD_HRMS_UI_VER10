using Hrms.Lite.Shared.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
  public interface ICoffBalanceRptService
    {
        public Task<CoffBalanceReport> GetList(Filter filter);
        public Task<CoffBalanceReport> GetCoffBalDetails(Guid empGI);
        public Task<DataSet> CoffBalanceReport_Excel(Filter filter);
        public Task<CoffBalance> GetCoffBalanceDetails(Guid EmployeeGI,string TYPE);
        public Task<CoffBalance> GetCoffLeaveBalance(Guid EmployeeGI);
    }
}
