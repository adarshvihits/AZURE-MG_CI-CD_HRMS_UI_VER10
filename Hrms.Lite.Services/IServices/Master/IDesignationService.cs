using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IDesignationService
    {
        public Task<Designation> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Designation Input);
        public Task<Designation> GetDesignationDetails(Guid DesignationGI,Guid LogGI,string type);
        public Task<ResponseEntity> Delete(Designation input);
        public Task<ResponseEntity> ApproveOrReject(Designation input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name,int code);
    }
}
