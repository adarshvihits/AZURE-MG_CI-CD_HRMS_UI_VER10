using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface INightAllowanceAmountSettingsService
    {
        public Task<NightAllowanceAmountSettings> GetList(int DepartmentCode, int PurposeCode, int GradeCode);
        public Task<ResponseEntity> Create(NightAllowanceAmountSettings input);
        public Task<NightAllowanceAmountSettings> GetDetails(int Code,int DepartmentCode, int PurposeCode, int GradeCode, string Type);
        public Task<ResponseEntity> Delete(NightAllowanceAmountSettings model);
    }
}
