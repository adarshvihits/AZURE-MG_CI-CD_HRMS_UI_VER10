using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IQualificationService
    {
        public Task<Qualification> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Qualification Input);
        public Task<Qualification> GetQualificationDetails(Guid QualificationGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Qualification input);
       public Task<ResponseEntity> ApproveOrReject(Qualification model);

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
