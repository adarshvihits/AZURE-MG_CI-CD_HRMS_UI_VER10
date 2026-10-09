using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IWorkLocationService
    {
        public Task<WorkLocation> GetList(string TabIndex);
        public Task<WorkLocation> GetWorkLocationDetails(Guid EmploymentGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(WorkLocation input);
        public Task<ResponseEntity> ApproveOrReject(WorkLocation model);
        public Task<ResponseEntity> Delete(WorkLocation model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
