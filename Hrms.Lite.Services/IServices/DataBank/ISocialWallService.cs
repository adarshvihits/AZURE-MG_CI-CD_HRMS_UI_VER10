using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ISocialWallService
    {
        public Task<SocialWall> SocialWallList(string TabIndex, Filter filter);
        public Task<ResponseEntity> HRSave(SocialWall model);
        public Task<ResponseEntity> Approve(SocialWall model);
        public Task<ResponseEntity> Delete(Guid SocialWallGI);
        public Task<SocialWall> SocialWallDetails(Guid SocialWallGI);
    }
}
