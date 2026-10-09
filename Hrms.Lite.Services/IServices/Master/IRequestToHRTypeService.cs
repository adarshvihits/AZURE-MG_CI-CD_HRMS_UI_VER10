using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IRequestToHRTypeService
    {
        public Task<RequestToHRType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(RequestToHRType input);
        public Task<RequestToHRType> GetDetails(Guid RequestToHrTypeGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(RequestToHRType model);
        public Task<ResponseEntity> ApproveOrReject(RequestToHRType model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
