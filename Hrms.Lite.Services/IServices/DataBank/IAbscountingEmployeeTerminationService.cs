using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IAbscountingEmployeeTerminationService
    {
        public Task<AbscondingReport> GetEmployeeList(Filter filter);
        public Task<DataSet> GetAbscondingEmployeeList_Excel(Filter filter);
        public Task<AbscondingReport> GetAbscondingEmployeeDetails(Guid EmployeeGI);
        public Task<AbscondingReport> GetAbscondingEmployeeSalaryDetails(Guid EmployeeGI, string LWD);
        public Task<ResponseEntity> Create(AbscondingReport input);
    }
}
