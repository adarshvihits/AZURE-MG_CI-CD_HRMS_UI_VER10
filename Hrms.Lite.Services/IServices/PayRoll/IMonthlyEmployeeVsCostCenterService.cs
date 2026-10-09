using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.PayRoll
{
  public interface IMonthlyEmployeeVsCostCenterService
    {
        public Task<DataSet> GetCostCenterEmployeeList(int MonthCode,Filter filter);
        public Task<MonthlyEmployeeVsCostCenter> GetEmpVsCostCenterEditList(int monthCode, Guid EmployeeGI);
        public Task<ResponseEntity> SaveMonthlyEmployeeVsCostCenter(MonthlyEmployeeVsCostCenter model);
        public Task<ResponseEntity> BatchUpdateEmployeeVsCostCenter(int MonthCode);
        public Task<DataSet> GetCostCenterEmployeeList_Excel(int MonthCode, Filter filter);
    }
}
