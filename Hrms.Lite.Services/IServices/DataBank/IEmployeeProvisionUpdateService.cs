using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeProvisionUpdateService
    {
        public Task<EmployeeProvisionUpdate> GetList(int AllowanceCode, Filter filterdata);
        public Task<ResponseEntity> Create(EmployeeProvisionUpdate input);
        public Task<EmployeeProvisionUpdate> GetEmployeeProvisionUpdateDetails(Guid EmployeeGI, int AllowanceCode);
        public Task<ResponseEntity> Delete(EmployeeProvisionUpdate model);
        public Task<DataSet> GetList_Excel(int AllowanceCode, Filter Filter);
    }
}
