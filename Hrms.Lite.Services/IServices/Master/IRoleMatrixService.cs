using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System.Data;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.Master
{
   public interface IRoleMatrixService
    {
        public Task<RoleMatrix> GetList(int RoleCode);
        public Task<ResponseEntity> SaveAuth(RoleMatrix RoleMatrix);
        public Task<RoleMatrix> GetSectionGrade(int EmpCode);  
    }
}
