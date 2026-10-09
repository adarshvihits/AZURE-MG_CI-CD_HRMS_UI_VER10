using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IUserApplicabilityAdvancedUserService
    {
        Task<UserApplicabilityAdvancedUser> GetList(int GroupingCode, int ClassificationCode);
        public Task<ResponseEntity> Save(UserApplicabilityAdvancedUser model);
        public Task<ResponseEntity> BatchSave(UserApplicabilityAdvancedUser Input);
    }
}
