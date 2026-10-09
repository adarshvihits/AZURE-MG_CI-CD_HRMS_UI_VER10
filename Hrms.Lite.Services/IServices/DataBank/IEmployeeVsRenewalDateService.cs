using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsRenewalDateService
    {
        public Task<EmployeeVsRenewalDate> GetList(int TabIndex, Filter filterdata);
        public Task<ResponseEntity> Create(EmployeeVsRenewalDate input);
        public Task<EmployeeVsRenewalDate> GetEmployeeVsRenewalDateDetails(Guid EmployeeGI, string Type);
        public Task<ResponseEntity> Delete(EmployeeVsRenewalDate model);
       
    }
}
