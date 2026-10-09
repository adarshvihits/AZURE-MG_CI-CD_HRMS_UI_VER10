using Hrms.Lite.Shared.Master;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IPlanningApprovalService
    {
        public Task<PlanningApproval> GetPlanningApprovalList(string TabIndex, int? PlanningPeriod);
    }
}
