using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IEmpVsMobileRegistrationService
    {
        public Task<EmpVsMobileRegistration> GetList(Filter filter);
        public Task<ResponseEntity> Delete(EmpVsMobileRegistration input);
        public Task<DataSet> EmployeeVsMobileDevice_Excel(Filter filter);

    }
}
