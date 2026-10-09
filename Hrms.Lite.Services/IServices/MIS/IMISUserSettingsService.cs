using Hrms.Lite.Shared.MIS;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.MIS
{
    public interface IMISUserSettingsService
    {
        public Task<MISUserSettings> GetList(Filter filter1);
        public Task<ResponseEntity> Create(MISUserSettings input);
        public Task<ResponseEntity> Delete(MISUserSettings input);
    }
}
