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
    public interface IDepartmentWiseProfitCenterService
    {
        public Task<DepartmentWiseProfitCenter> GetList(int groupCompanyCode);

        public Task<ResponseEntity> Save(DepartmentWiseProfitCenter model);

        public Task<DepartmentWiseProfitCenter> Edit(int groupCompanyCode,int DepartmentCode);
        public Task<DataSet> GetDepartmentWiseProfitCenter_Excel(int groupCompanyCode);
    }
}
