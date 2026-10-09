using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsNpsService
    {
        public Task<EmployeeVsNps> GetList(int TabIndex, Filter filterdata);
        public Task<ResponseEntity> Create(EmployeeVsNps input);
        public Task<EmployeeVsNps> GetEmployeeVsNpsDetails(Guid EmployeeGI, string Type);
        public Task<ResponseEntity> Delete(EmployeeVsNps model);
        public Task<DataSet> GetList_Excel(int TabIndex, Filter Filter);
    }
}
