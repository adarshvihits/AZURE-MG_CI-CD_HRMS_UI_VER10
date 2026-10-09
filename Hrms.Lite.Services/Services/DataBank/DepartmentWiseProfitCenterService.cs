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
    public class DepartmentWiseProfitCenterService : IDepartmentWiseProfitCenterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DepartmentWiseProfitCenterService(IServiceBase httpService)
        {
            _baseURL = "DepartmentWiseProfitCenter";
            _httpService = httpService;
        }

        public async Task<DepartmentWiseProfitCenter> GetList(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DepartmentWiseProfitCenter>($"{_baseURL}/List?groupCompanycode={groupCompanyCode}");
        }

        public async Task<ResponseEntity> Save(DepartmentWiseProfitCenter model)
        {
            return await _httpService.PostAsync<ResponseEntity, DepartmentWiseProfitCenter>($"{_baseURL}/Save", model);


        }


        public Task<DepartmentWiseProfitCenter> Edit(int groupCompanyCode,int  DepartmentCode)
        {
            return _httpService.GetAsync<DepartmentWiseProfitCenter>($"{_baseURL}/Edit?groupCompanyCode={groupCompanyCode}&DepartmentCode={DepartmentCode}");

        }
        public async Task<DataSet> GetDepartmentWiseProfitCenter_Excel(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetDepartmentWiseProfitCenter_Excel?groupCompanycode={groupCompanyCode}");
        }
    }
}
