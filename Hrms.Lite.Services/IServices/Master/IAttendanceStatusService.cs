using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IAttendanceStatusService
    {
        public Task<AttendanceStatus> GetList(string TabIndex);
        public Task<ResponseEntity> Create(AttendanceStatus input);
        public Task<AttendanceStatus> GetAttendanceStatusDetails(Guid AttendanceStatusGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(AttendanceStatus model);
        public Task<ResponseEntity> Delete(AttendanceStatus model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
