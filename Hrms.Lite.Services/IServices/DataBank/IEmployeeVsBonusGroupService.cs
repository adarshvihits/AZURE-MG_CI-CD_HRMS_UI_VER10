using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsBonusGroupService
    {
        public Task<EmployeeVsBonusGroup> GetEmployeeList(int TabIndex, Filter filter);
        public Task<ResponseEntity> Save(EmployeeVsBonusGroup input);
        public Task<DataSet> GetEmployeeList_Excel(int TabIndex, Filter filter);        
        public Task<ResponseEntity> EmployeeVsBonusGroupBatchUpdate(Filter filter, int BonusGroup, int TabIndex);
    }
}
