using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IEmployeeVsWorkLocationService
    {
        public Task<EmployeeVsWorkLocation> GetEmployeeVsWorkLocationList(Filter filter);
        public Task<ResponseEntity> SaveEmployeeVsWorkLocation(EmployeeVsWorkLocation model);
        public Task<DataSet> GetEmployeeVsWorkLocationList_Excel(Filter filter);
        
    }
}
