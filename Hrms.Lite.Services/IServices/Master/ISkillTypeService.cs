using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISkillTypeService
    {
        public Task<SkillType> GetList(string TabIndex);
        public Task<ResponseEntity> Save(SkillType Input);
        public Task<SkillType> GetSkillTypeDetails(Guid SkillTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(SkillType model);
        public Task<ResponseEntity> Delete(SkillType model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
