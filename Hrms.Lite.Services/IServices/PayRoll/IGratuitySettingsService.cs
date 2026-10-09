using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IGratuitySettingsService
    {
        public Task<GratuitySettings> GetGratuitySettingsDetails();
        public Task<ResponseEntity> SaveGratuitySettings(GratuitySettings model);
    }
}
