using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsVDAGroupService
    {
        public Task<VDAGroup> GetList(string TabIndex, Filter filter);
        public Task<ResponseEntity> Create(VDAGroup model);
        public Task<ResponseEntity> BatchUpdate(VDAGroup model);
        public Task<DataSet> EmployeeVDAGroup_Excel(string TabIndex, Filter Filter);
        public Task<ResponseEntity<List<VDAGroup>>> ImportFromExcel(string TabIndex,VDAGroup model);
    }
}
