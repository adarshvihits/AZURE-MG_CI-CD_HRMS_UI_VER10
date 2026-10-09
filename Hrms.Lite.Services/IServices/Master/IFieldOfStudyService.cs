using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IFieldOfStudyService
    {
        public Task<FieldOfStudy> GetList(string TabIndex);
        public Task<ResponseEntity> Create(FieldOfStudy input);
        public Task<FieldOfStudy> GetFieldOfStudyDetails(Guid FieldOfStudyGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(FieldOfStudy model);
        public Task<ResponseEntity> Delete(FieldOfStudy model);

    }
}
