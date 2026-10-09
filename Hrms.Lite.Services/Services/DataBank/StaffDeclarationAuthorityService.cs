
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class StaffDeclarationAuthorityService : IStaffDeclarationAuthorityService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public StaffDeclarationAuthorityService(IServiceBase httpService)
        {
            _baseURL = "StaffDeclarationAuthority";
            _baseURL1 = "Common";
            _httpService = httpService;

        }


        Task<StaffDeclarationAuthority>IStaffDeclarationAuthorityService.GetList(Filter filterdata)
        {
            return _httpService.PostAsync<StaffDeclarationAuthority, Filter>($"{_baseURL}/GetList", filterdata);
        }



        public Task<ResponseEntity> Create(StaffDeclarationAuthority input)
        {
            return _httpService.PostAsync<ResponseEntity, StaffDeclarationAuthority>($"{_baseURL}/Create", input);
        }




        public async Task<DataSet> GetList_Excel( Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", Filter);
        }
    }
}
