using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAttendanceRequestReasonService
    {
        public Task<AttendanceRequestReason> GetList(string TabIndex);
        public Task<AttendanceRequestReason> GetLeaveTypeSettings();
        public Task<ResponseEntity> Create(AttendanceRequestReason input);
        public Task<ResponseEntity> Delete(AttendanceRequestReason model);
        public Task<ResponseEntity> ApproveOrReject(AttendanceRequestReason model);
        public Task<AttendanceRequestReason> GetDetails(Guid ReasonGI, Guid LogGI, string Type);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
