using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IPTSettingsService
    {
        public Task<PTSettings> PTSettingsList(int ptGroup);
        public Task<ResponseEntity> Save(PTSettings model);
        public Task<PTSettings> GetPTSettingsAllowance(int ptGroup);
        public Task<ResponseEntity> AllowanceSave(PTSettings model);
    }
}
