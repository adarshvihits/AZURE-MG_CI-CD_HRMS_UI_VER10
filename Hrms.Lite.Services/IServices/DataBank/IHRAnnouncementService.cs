using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IHRAnnouncementService
    {
        public Task<HRAnnouncement> GetList();
        Task<HRAnnouncement> GetCombinationDetails(int id);
        public Task<ResponseEntity> AnnouncementSave(HRAnnouncement model);
        public Task<ResponseEntity> AnnouncementUpdate(HRAnnouncement model);
        public Task<HRAnnouncement> GetAnnouncementDetails(Guid AnnouncementGI);
        public Task<ResponseEntity> Delete(HRAnnouncement model);
    }
}
