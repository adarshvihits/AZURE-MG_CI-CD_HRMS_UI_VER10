using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IRewardsAndRecognitionService
    {
        Task<IList<RewardandRecognitionTabs>> GetRewardsAndRecognitionTabs();
        public Task<RewardsAndRecognition> GetRewardsAndRecognitionList( int Tabindex, int SearchId,int? RewardCode,Filter filter);

        public Task<ResponseEntity> NewRequest(RewardsRequest model);
        public Task<RewardsRequest> GetRewardsLogDetails(Guid RewardsAndRecognitionGI);
        public Task <IList<RewardsLog>> GetRewardLogList(Guid EmployeeGI);
        public Task<RewardsAndRecognition> GetRewardsAndRecognitionDetails(Guid RewardsAndRecognitionGI);
        public Task<RewardsAndRecognition> GetEmployeeSummary(Guid EmployeeGI,  int SearchId );
     
        public Task<ResponseEntity> Approve(RewardsRequest model);
        public Task<ResponseEntity> Delete(Guid RewardsAndRecognitionGI);
        public Task<decimal> GetRewardPoints(int PointCriteriaCode);

    }
}
