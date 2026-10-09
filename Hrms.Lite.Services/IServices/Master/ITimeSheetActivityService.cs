using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface ITimeSheetActivityService
    {
        public Task<TimeSheetActivity> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TimeSheetActivity input);
        public Task<TimeSheetActivity> GetActivityDetails(Guid ActivityGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(TimeSheetActivity model);
        public Task<ResponseEntity> Delete(TimeSheetActivity model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
