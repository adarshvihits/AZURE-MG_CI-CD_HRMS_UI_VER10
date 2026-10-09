using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDueClearanceSettingsService
    {
        public Task<DueClearanceSettings> GetDueClearanceSettingsList();
        public Task<DueClearanceSettings> GetDueClearanceSettingsDetails(Guid ParaGI);
        public Task<ResponseEntity> Create(DueClearanceSettings model);
        public Task<ResponseEntity> Delete(Guid ParaGI);
    }
}
