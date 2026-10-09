using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AccountService(IServiceBase httpService)
        {
            _baseURL = "Account";
            _httpService = httpService;
        }
        async Task<CustomSoftware> IAccountService.GetSoftwareLogo()
        {
            return await _httpService.GetAsync<CustomSoftware>($"{_baseURL}/GetSoftwareLogo");
        }

      
        async Task<ResponseEntity<string>> IAccountService.Authenticate(Login input)
        {
            return await _httpService.PostAsync<ResponseEntity<string>, Login>($"{_baseURL}/Authenticate", input);
        }async Task<ResponseEntity<string>> IAccountService.Login(Login input)
        {
            return await _httpService.PostAsync<ResponseEntity<string>, Login>($"{_baseURL}/Login", input);
        }
        async Task<ResponseEntity<string>> IAccountService.LogHistory(Login input)
        {
            return await _httpService.PostAsync<ResponseEntity<string>, Login>($"{_baseURL}/LogHistory", input);
        }
        async Task<UserRoleDetails> IAccountService.GetLogOutUserRoleDetails(int UserRoleCode)
        {
            return await _httpService.GetAsync<UserRoleDetails>($"{_baseURL}/GetLogOutUserRoleDetails?UserRoleCode={UserRoleCode}");
        }
        async Task<List<Dropdown>> IAccountService.GetGroupCompanyDropDown(Login input)
        {
            return await _httpService.PostAsync<List<Dropdown>,Login>($"{_baseURL}/GetGroupCompanyDropDown", input);
        }
    }
}
