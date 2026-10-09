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
    public class SocialWallService : ISocialWallService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SocialWallService(IServiceBase httpService)
        {
            _baseURL = "SocialWall";
            _httpService = httpService;
        }
        public async Task<ResponseEntity> HRSave(SocialWall model)
        {
            return await _httpService.PostAsync<ResponseEntity, SocialWall>($"{_baseURL}/HRMSSave", model);
        }
        public async Task<ResponseEntity> Approve(SocialWall model)
        {
            return await _httpService.PostAsync<ResponseEntity, SocialWall>($"{_baseURL}/Approve", model);
        }
        public async Task<SocialWall> SocialWallList(string TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<SocialWall, Filter>($"{_baseURL}/SocialWallList?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> Delete(Guid SocialWallGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/Delete?SocialWallGI={SocialWallGI}");
        }
        public async Task<SocialWall> SocialWallDetails(Guid SocialWallGI)
        {
            return await _httpService.GetAsync<SocialWall>($"{_baseURL}/SocialWallDetails?SocialWallGI={SocialWallGI}");
        }
    }
}
