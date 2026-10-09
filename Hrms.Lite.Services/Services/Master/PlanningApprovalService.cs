using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class PlanningApprovalService : IPlanningApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PlanningApprovalService(IServiceBase httpService)
        {
            _baseURL = "PlanningApprovals";
            _httpService = httpService;
        }
        async Task<PlanningApproval> IPlanningApprovalService.GetPlanningApprovalList(string TabIndex, int? PlanningPeriod)
        {
            return await _httpService.GetAsync<PlanningApproval>($"{_baseURL}/GetPlanningApprovalList?TabIndex={TabIndex}&PlanningPeriod={PlanningPeriod}");
        }
    }
}
