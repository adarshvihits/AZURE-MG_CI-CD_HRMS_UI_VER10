using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
  public  class GrievanceAuthorityService: IGrievanceAuthorityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public GrievanceAuthorityService(IServiceBase httpService)
        {
            _baseURL = "GrievanceAuthority";
            _httpService = httpService;
        }

        public async Task<GrievanceAuthority> GetDetails(int GrievanceCatID)
        {
            return await _httpService.GetAsync<GrievanceAuthority>($"{_baseURL}/GrievanceAuthorityDetails?GrievanceCatID={GrievanceCatID}");
        }

        public async Task<GrievanceAuthority> GetList()
        {
            return await _httpService.GetAsync<GrievanceAuthority>($"{_baseURL}/List");
        }

        public async Task<GrievanceAuthority> GetRefNumber()
        {
            return await _httpService.GetAsync<GrievanceAuthority>($"{_baseURL}/GetRefNo");
        }

       
        public Task<ResponseEntity> Save(GrievanceAuthority input)
        {
            return _httpService.PostAsync<ResponseEntity, GrievanceAuthority>($"{_baseURL}/Create", input);
        }
    }
}
