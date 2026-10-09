using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IChangeAuthorityService
    {
        public Task<ChangeAuthority> GetList(int WorkflowCode,Filter filter);
        public Task<ChangeAuthority> GetAuthorityDetails(int TrnCode, int WorkflowCode);
        public Task<ResponseEntity> AuthorityUpdate(int TrnCode, int LevelCode, int AuthorityCode, int HDCode, int WorkflowCode);
    }
}
