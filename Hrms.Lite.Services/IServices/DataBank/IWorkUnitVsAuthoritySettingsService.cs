using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IWorkUnitVsAuthoritySettingsService
    {
        public Task<WorkUnitVsAuthoritySettings> GetList();
        public Task<WorkUnitVsAuthoritySettings> GetUnitVsAuthDetails(int WorkunitCode,int RequestTypeCode);
        public Task<ResponseEntity> Save(WorkUnitVsAuthoritySettings input);
        public Task<WorkUnitVsAuthoritySettings> GetApplicableShift(int WorkunitCode);
        public Task<ResponseEntity> ApplicableShiftSave(WorkUnitVsAuthoritySettings input);
    }
}
