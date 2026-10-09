using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IRecruitmentSettingsService
    {
        public Task<RecruitmentSettings> GetList();
        public Task<RecruitmentSettings> GetDetailsList(Guid DesignationGI);
        public Task<ResponseEntity> Create(RecruitmentSettings input);


    }
}
