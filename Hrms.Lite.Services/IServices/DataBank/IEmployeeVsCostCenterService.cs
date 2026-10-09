using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsCostCenterService
    {
        public Task<CostCenter> GetList(string TabIndex, Filter filter);
        public Task<ResponseEntity> Create(CostCenter model);
        public Task<ResponseEntity> BatchUpdate(CostCenter model);
        public Task<DataSet> EmployeeCostCenter_Excel(string TabIndex,Filter Filter);
        public Task<ResponseEntity<List<CostCenter>>> ImportFromExcel(string TabIndex,CostCenter model);
    }
}
