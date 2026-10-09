using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IUserService
    {
        public Task<User> GetUserList(string Tabindex,Filter filter);
        public Task<User> GetUserDetails(Guid UserMasterGI,Guid LogGI, string Type);
        public Task<User> GetApplicableMenus(int UserRoleCode);
        public Task<ResponseEntity> UserSave(User model);
        public Task<ResponseEntity> ApproveOrReject(User model);
        public Task<ResponseEntity> UserDelete(Guid UserMaseterGI);
        Task<DataSet> GetUserList_Excel(string TabIndex, Filter filterdata);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
