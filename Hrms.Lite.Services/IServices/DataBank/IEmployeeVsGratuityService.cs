using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsGratuityService
    {

        public Task<EmployeeVsGratuity> GetList(int TabIndex, Filter filterdata, string FromDate, string ToDate);
        public Task<ResponseEntity> Create(EmployeeVsGratuity input);
        public Task<EmployeeVsGratuity> GetEmployeeVsGratuityDetails(Guid EmployeeGI, string Type);
       
        public Task<DataSet> GetList_Excel(int TabIndex, Filter Filter, string FromDate, string ToDate);
    }
}
