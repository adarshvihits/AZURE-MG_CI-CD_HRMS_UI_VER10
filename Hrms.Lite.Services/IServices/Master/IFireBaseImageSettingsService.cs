using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IFireBaseImageSettingsService
    {
        public Task<FireBaseImageSettings> GetList();
        public Task<FireBaseImageSettings> GetDetails(int Code);
        Task<ResponseEntity> Save(FireBaseImageSettings Input);

        public Task<ResponseEntity> MobileGeneralSettingsSave(FireBaseImageSettings model);
        public Task<FireBaseImageSettings> GetMobileGeneralSettingsDetails(string TabIndex);
    }
}
