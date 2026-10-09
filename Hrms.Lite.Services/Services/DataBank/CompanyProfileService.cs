using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
  public  class CompanyProfileService:ICompanyProfileService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CompanyProfileService(IServiceBase httpService)
        {
            _baseURL = "CompanyProfile";
            _httpService = httpService;
        }
        async Task<CompanyProfile> ICompanyProfileService.GetCompanyProfile(Filter filter, int GroupCompanyCode)
        {
            return await _httpService.PostAsync<CompanyProfile, Filter>($"{_baseURL}/GetCompanyProfile?GroupCompanyCode={GroupCompanyCode}",filter);
        }
        public async Task<CompanyProfile> GetCompanyProfileDetails(int CODE, string TYPE,Filter filter,int GroupCompanyCode)
        {
            return await _httpService.PostAsync<CompanyProfile,Filter>($"{_baseURL}/GetCompanyProfileDetails?CODE={CODE}&TYPE={TYPE}&GroupCompanyCode={GroupCompanyCode}",filter);
        }

      public async Task<DataSet> GetCompanyProfileEmployee_Excel(int CODE, string TYPE, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCompanyProfileEmployee_Excel?CODE={CODE}&TYPE={TYPE}", filter);

        }
    }
}
