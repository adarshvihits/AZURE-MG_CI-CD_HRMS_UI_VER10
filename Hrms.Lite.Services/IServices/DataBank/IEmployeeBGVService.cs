using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeBGVService
    {
        public Task<EmployeeBGV> GetEmployeeList(Filter filter,int TabIndex);
        public Task<EmployeeBGV> GetApplicantDetails(Guid EmployeeGI,int ApplicantCode);
        public Task<ResponseEntity> SaveBGV(EmployeeBGV model);
        public Task<DataSet> GetEmployeeList_Excel(Filter filter, int TabIndex);
    }
}
