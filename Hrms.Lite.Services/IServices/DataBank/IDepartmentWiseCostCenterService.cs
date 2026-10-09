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
    public interface IDepartmentWiseCostCenterService
    {
        public Task<DepartmentWiseCostCenter> GetList(int groupCompanyCode);

        public Task<ResponseEntity> Save(DepartmentWiseCostCenter model);
        public Task<DataSet> GetDepartmentWiseCostCenter_Excel(int groupCompanyCode);
    }
}
