using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IUHIDService
    {
        public Task<UHID> GetUHIDList(Filter filter);
        public Task<UHID> GetEmployeeDetails( Guid EmployeeGI);
        public Task<ResponseEntity> EmployeeWiseUpdate(UHID Input);
        public Task<DataSet> UHIDList_Excel(string Type, Filter filter);
        public Task<UHID> GetUHIDDetails(string Type, Filter filter1);

    }
}
