using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface Ip2pRecognitionService
    {
        public Task<p2pRecognition> GetList(int radiomode);
        public Task<p2pRecognition> Getp2pRecognitionDetails(Guid EmployeeGI, int RewardCode);

        public Task<ResponseEntity> Create(p2pRecognition input);
        public Task<decimal> GetRewardPoints(int PointCriteriaCode);
    }
}
