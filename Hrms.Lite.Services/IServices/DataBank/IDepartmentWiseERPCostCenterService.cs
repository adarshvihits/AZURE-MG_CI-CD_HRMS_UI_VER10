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
    public interface IDepartmentWiseERPCostCenterService
    {
        public Task<DepartmentWiseERPCostCenter> GetList(int groupCompanyCode);

        public Task<ResponseEntity> Save(DepartmentWiseERPCostCenter model);
        public Task<DataSet> GetDepartmentWiseERPCostCenter_Excel(int groupCompanyCode);
    }
}
