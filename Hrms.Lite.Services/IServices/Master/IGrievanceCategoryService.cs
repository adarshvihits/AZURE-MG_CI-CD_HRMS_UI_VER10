using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public  interface IGrievanceCategoryService
    {
        public Task<GrievanceCategory> GetList(string TabIndex);
        public Task<GrievanceCategory> GetGrievanceCatDetails(Guid GrievanceGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(GrievanceCategory input);
        public Task<ResponseEntity> Delete(GrievanceCategory model);
        public Task<ResponseEntity> ApproveOrReject(GrievanceCategory model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }

}
