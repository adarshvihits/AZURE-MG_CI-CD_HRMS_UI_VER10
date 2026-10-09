using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ITimeSheetProjectService
    {
        public Task<TimeSheetProject> GetList(string TabIndex);
        public Task<ResponseEntity> Create(TimeSheetProject input);
        public Task<TimeSheetProject> GetProjectDetails(Guid ProjectGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(TimeSheetProject model);
        public Task<ResponseEntity> Delete(TimeSheetProject model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
