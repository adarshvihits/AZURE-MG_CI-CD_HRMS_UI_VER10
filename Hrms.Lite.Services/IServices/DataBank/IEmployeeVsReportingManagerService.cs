using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsReportingManagerService
    {
        public Task<EmployeeVsReportingManager> GetList(int TabIndex, Filter filterdata);
        public Task<ResponseEntity> Create(EmployeeVsReportingManager input);
        public Task<EmployeeVsReportingManager> GetEmployeeVsReportingManagerDetails(Guid EmployeeGI);
        public Task<DataSet> GetList_Excel(int TabIndex, Filter Filter);
    }
}

