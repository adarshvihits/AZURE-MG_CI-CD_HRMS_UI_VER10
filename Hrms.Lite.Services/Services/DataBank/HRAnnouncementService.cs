using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class HRAnnouncementService: IHRAnnouncementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRAnnouncementService(IServiceBase httpService)
        {
            _baseURL = "HRAnnouncement";
            _httpService = httpService;
        }
        public async Task<HRAnnouncement> GetList()
        {
            return await _httpService.GetAsync<HRAnnouncement>($"{_baseURL}/List");
        }

        public async Task<HRAnnouncement> GetCombinationDetails(int id)
        {
            return await _httpService.GetAsync<HRAnnouncement>($"{_baseURL}/CombinationDetails?Typecode={id}");
        }

        public async Task<ResponseEntity> AnnouncementSave(HRAnnouncement model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAnnouncement>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> AnnouncementUpdate(HRAnnouncement model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAnnouncement>($"{_baseURL}/Update", model);
        }
        public async Task<HRAnnouncement> GetAnnouncementDetails(Guid AnnouncementGI)
        {
            return await _httpService.GetAsync<HRAnnouncement>($"{_baseURL}/Details?AnnouncementGI={AnnouncementGI}");
        }

        public async Task<ResponseEntity>Delete(HRAnnouncement model)
        {
            return await _httpService.PostAsync<ResponseEntity,HRAnnouncement>($"{_baseURL}/Delete", model);
        }
    }
}
