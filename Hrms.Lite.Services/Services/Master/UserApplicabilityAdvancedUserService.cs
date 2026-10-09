using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
   public class UserApplicabilityAdvancedUserService: IUserApplicabilityAdvancedUserService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public UserApplicabilityAdvancedUserService(IServiceBase httpService)
        {
            _baseURL = "UserApplicabilityAdvancedUser";
            _httpService = httpService;
        }
        public async Task<UserApplicabilityAdvancedUser> GetList(int GroupingCode, int ClassificationCode)
        {
            return await _httpService.GetAsync<UserApplicabilityAdvancedUser>($"{_baseURL}/List?GroupingCode={GroupingCode}&ClassificationCode={ClassificationCode}");
        }
        public Task<ResponseEntity> Save(UserApplicabilityAdvancedUser input)
        {
            return _httpService.PostAsync<ResponseEntity, UserApplicabilityAdvancedUser>($"{_baseURL}/Save", input);
        }

        public async Task<ResponseEntity> BatchSave(UserApplicabilityAdvancedUser Input)
        {
            return await _httpService.PostAsync<ResponseEntity, UserApplicabilityAdvancedUser>($"{_baseURL}/BatchSave", Input);
        }
    }
}
