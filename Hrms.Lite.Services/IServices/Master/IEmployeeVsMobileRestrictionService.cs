using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IEmployeeVsMobileRestrictionService
    {
        Task<EmployeeVsMobileRestriction> GetList(int TabIndex, Filter filter);
        Task<ResponseEntity> RestrictedEmpSave(EmployeeVsMobileRestriction model);
        Task<DataSet> GetRestrictedEmp_Excel(int TabIndex, Filter filter);
    }
}
