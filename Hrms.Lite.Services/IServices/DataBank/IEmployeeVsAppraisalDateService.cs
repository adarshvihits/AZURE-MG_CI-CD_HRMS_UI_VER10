using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsAppraisalDateService
    {
        Task<EmployeeVsAppraisalDate> GetList(Filter filter);
        Task<EmployeeVsAppraisalDate> GetAppraisalDateDetails(Guid EmployeeGI);
        public Task<ResponseEntity> Create(EmployeeVsAppraisalDate model);
        Task<DataSet> GetList_Excel(Filter filter);
    }
}
