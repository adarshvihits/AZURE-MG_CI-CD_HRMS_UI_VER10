using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class UserService:IUserService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public UserService(IServiceBase httpService)
        {
            _baseURL = "User";
            _baseURL1 = "Common";
            _httpService = httpService;
        }






        public async Task<DataSet> GetUserList_Excel(string TabIndex, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetUserList_Excel?Tabindex={TabIndex}", filterdata);
        }
        async Task<User> IUserService.GetUserDetails(Guid UserMasterGI,Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<User>($"{_baseURL}/GetUserDetails?UserMasterGI={UserMasterGI}&LogGI={LogGI}&Type={Type}");
        }
        async Task<User> IUserService.GetUserList(string Tabindex,Filter filter)
        {
            return await _httpService.PostAsync<User,Filter>($"{_baseURL}/GetUserList?Tabindex={Tabindex}",filter);
        }
        async Task<User> IUserService.GetApplicableMenus(int UserRoleCode)
        {
            return await _httpService.GetAsync<User>($"{_baseURL}/GetApplicableMenus?UserRoleCode={UserRoleCode}");
        }
        async Task<ResponseEntity> IUserService.UserSave(User model)
        {
            return await _httpService.PostAsync<ResponseEntity, User>($"{_baseURL}/UserSave", model);
        }
        async Task<ResponseEntity> IUserService.ApproveOrReject(User model)
        {
            return await _httpService.PostAsync<ResponseEntity, User>($"{_baseURL}/ApproveOrReject", model);
        }
        async Task<ResponseEntity> IUserService.UserDelete(Guid UserMasterGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/{UserMasterGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
