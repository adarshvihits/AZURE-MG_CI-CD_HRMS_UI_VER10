using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IFieldOfCertificationService
    {
        public Task<FieldOfCertification> GetList(string TabIndex);
        public Task<ResponseEntity> Create(FieldOfCertification input);
        public Task<FieldOfCertification> GetFieldOfCertificationDetails(Guid FieldOfCertificationGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(FieldOfCertification model);
        public Task<ResponseEntity> Delete(FieldOfCertification input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
