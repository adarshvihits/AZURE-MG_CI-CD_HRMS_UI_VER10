using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISkillService
    {
        public Task<Skill> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Skill input);
        public Task<Skill> GetSkillDetails(Guid SkillGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Skill model);
        public Task<ResponseEntity> Delete(Skill model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
