using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IADDeactivateService
    {
        public Task<ADDeactivate> GetList(int TabIndex, Filter filterdata);
        public Task<ResponseEntity> Create(ADDeactivate input);
        public Task<ADDeactivate> GetADDeactivateDetails(Guid EmployeeGI, string Type);

        public Task<DataSet> GetList_Excel(int TabIndex, Filter Filter);
    }
}
