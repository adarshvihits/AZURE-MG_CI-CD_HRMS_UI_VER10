using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICircleService
    {
        public Task<Circle> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Circle input);
        public Task<Circle> GetCircleDetails(Guid CircleGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Circle model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<ResponseEntity> ApproveOrReject(Circle model);
    }
}
