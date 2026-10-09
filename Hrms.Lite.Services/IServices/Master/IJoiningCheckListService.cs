using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IJoiningCheckListService
    {
        public Task<JoiningCheckList> GetList(string TabIndex);
        public Task<ResponseEntity> Save(JoiningCheckList Input);
        public Task<JoiningCheckList> GetJoiningCheckListDetails(Guid JoiningCheckListGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(JoiningCheckList input);
        public Task<ResponseEntity> ApproveOrReject(JoiningCheckList model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);


    }
}
