using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IHolidayGroupService
    {
        public Task<HolidayGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(HolidayGroup input);
        public Task<HolidayGroup> GetHolidayGroupDetails(Guid HolidayGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(HolidayGroup model);
        public Task<ResponseEntity> Delete(HolidayGroup input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
