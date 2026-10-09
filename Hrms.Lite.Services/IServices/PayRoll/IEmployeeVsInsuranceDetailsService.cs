using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IEmployeeVsInsuranceDetailsService
    {
        public Task<EmployeeVsInsuranceDetails> GetEmployeeList(int Policy, Filter filter);
        public Task<EmployeeVsInsuranceDetails> GetEmployeeInsuranceDetails(int Policy);
        public Task<EmployeeVsInsuranceDetails> GetEmployeeVsInsuranceDetails(Guid EmployeeGUID, int PolicyCode);
        public Task<ResponseEntity> Create(EmployeeVsInsuranceDetails input);
        public Task<ResponseEntity> Update(EmployeeVsInsuranceDetails input);
        public Task<DataSet> GetEmployeeVsInsuranceDetails_Excel(int Policy, Filter filter);
        public Task<ResponseEntity<List<EmployeeVsInsuranceDetails>>> ImportFromExcel(EmployeeVsInsuranceDetails model);
    }
}
