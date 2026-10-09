using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.Services.Master
{
   public class RoleMatrixService: IRoleMatrixService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RoleMatrixService(IServiceBase httpService)
        {
            _baseURL = "RoleMatrix";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<RoleMatrix> GetList(int RoleCode)
        {
            return await _httpService.GetAsync<RoleMatrix>($"{_baseURL}/GetList?RoleCode={RoleCode}");
        }
        public Task<ResponseEntity> SaveAuth(RoleMatrix RoleMatrix)
        {
            return _httpService.PostAsync<ResponseEntity, RoleMatrix>($"{_baseURL}/SaveAuth", RoleMatrix);
        }
        public async Task<RoleMatrix> GetSectionGrade(int EmpCode)
        {
            return await _httpService.GetAsync<RoleMatrix>($"{_baseURL}/GetSectionGrade?EmpCode={EmpCode}");
        }

    }

}
