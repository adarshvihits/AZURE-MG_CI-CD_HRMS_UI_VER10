using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ILocationMarkingApplicabilityService
    {
        Task<LocationMarkingApplicability> GetList(int TabIndex, Filter filter);
        Task<ResponseEntity> ApplicableEmpSave(LocationMarkingApplicability model);
        Task<DataSet> GetApplicableEmp_Excel(int TabIndex, Filter filter);

    }
}
