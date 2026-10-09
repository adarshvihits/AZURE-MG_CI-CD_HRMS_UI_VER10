using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.Services.IServices.TDS
{
   public interface IForm16SettingsService
    {
        public Task<ResponseEntity> SaveForm16Settings(Form16Settings input);
        public Task<Form16Settings> GetFinYearDetails(int AssessmentYear);
    }
}
