using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Account
{
 public   interface IAccountService
    {
        Task<CustomSoftware> GetSoftwareLogo();
        public Task<ResponseEntity<string>> Authenticate(Login  input);
        public Task<ResponseEntity<string>> Login(Login  input);
        public Task<ResponseEntity<string>> LogHistory(Login input);
        public Task<UserRoleDetails> GetLogOutUserRoleDetails(int UserRoleCode);
        public Task<List<Dropdown>> GetGroupCompanyDropDown(Login input);
    }
}
