using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class DepartmentWiseERPCostCenterService : IDepartmentWiseERPCostCenterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DepartmentWiseERPCostCenterService(IServiceBase httpService)
        {
            _baseURL = "DepartmentWiseERPCostCenter";
            _httpService = httpService;
        }

        public async Task<DepartmentWiseERPCostCenter> GetList(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DepartmentWiseERPCostCenter>($"{_baseURL}/List?groupCompanycode={groupCompanyCode}");
        }

        public async Task<ResponseEntity> Save(DepartmentWiseERPCostCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity, DepartmentWiseERPCostCenter>($"{_baseURL}/Save", model);
        }
        public async Task<DataSet> GetDepartmentWiseERPCostCenter_Excel(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetDepartmentWiseERPCostCenter_Excel?groupCompanycode={groupCompanyCode}");
        }
    }
}
