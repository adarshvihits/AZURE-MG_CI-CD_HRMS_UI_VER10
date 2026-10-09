using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICompanyGeneralSettingsService
    {
        public Task<ResponseEntity> CompanyGeneralSettingsSave(CompanyGeneralSettings model);
        public Task<CompanyGeneralSettings> GetCompanyGeneralSettingsDetails();

    }
}
