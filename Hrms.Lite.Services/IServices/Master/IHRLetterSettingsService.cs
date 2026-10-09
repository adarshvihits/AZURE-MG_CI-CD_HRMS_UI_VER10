using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IHRLetterSettingsService
    {
        public Task<ResponseEntity> HRLetterSettingsSave(HRLetterSettings model,int LetterType);
        public Task<HRLetterSettings> GetHRLetterSettingsDetails(int LetterType);
    }
}
