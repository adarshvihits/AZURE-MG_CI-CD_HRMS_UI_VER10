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
   public class DepartmentWiseCostCenterService : IDepartmentWiseCostCenterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DepartmentWiseCostCenterService(IServiceBase httpService)
        {
            _baseURL = "DepartmentWiseCostCenter";
            _httpService = httpService;
        }

        public async Task<DepartmentWiseCostCenter> GetList(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DepartmentWiseCostCenter>($"{_baseURL}/List?groupCompanycode={groupCompanyCode}");
        }

        public async Task<ResponseEntity> Save(DepartmentWiseCostCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity, DepartmentWiseCostCenter>($"{_baseURL}/Save", model);
        }
        public async Task<DataSet> GetDepartmentWiseCostCenter_Excel(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetDepartmentWiseCostCenter_Excel?groupCompanycode={groupCompanyCode}");
        }
    }
}
