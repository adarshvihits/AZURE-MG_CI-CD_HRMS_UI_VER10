using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IRecruitmentdriveService
    {
        public Task<Recruitmentdrive> GetList(string TabIndex);
        public Task<Recruitmentdrive> GetResultList(Guid RecruitmentDriveGI, Guid LogGI, string Mode);
        public Task<ResponseEntity> Delete(Guid gi);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<ResponseEntity> ApproveOrReject(Recruitmentdrive model);

        public Task<ResponseEntity> Save(Recruitmentdrive model);

    }
}
