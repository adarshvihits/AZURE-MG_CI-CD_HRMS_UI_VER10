using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IHrSettingsService
    {
        public Task<HrSettings> GetList();

        public Task<ResponseEntity> Save(HrSettings model);
    }
}
