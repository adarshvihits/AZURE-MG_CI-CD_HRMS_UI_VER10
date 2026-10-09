using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
  public  interface IGrievanceAuthorityService
    {
        public Task<GrievanceAuthority> GetList();
        public Task<GrievanceAuthority> GetDetails(int GrievanceCatID);
        public Task<ResponseEntity> Save(GrievanceAuthority input);
        public Task<GrievanceAuthority> GetRefNumber();
    }
}
