
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
    public class NewBranchPositionCreationService : INewBranchPositionCreationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public NewBranchPositionCreationService(IServiceBase httpService)
        {
            _baseURL = "NewBranchPositionCreation";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        async Task<NewBranchPositionCreationViewModel> INewBranchPositionCreationService.GetNewBranchPositionCreationList(string TabIndex, int? PlanningPeriod)
        {
            return await _httpService.GetAsync<NewBranchPositionCreationViewModel>($"{_baseURL}/GetNewBranchPositionCreationList?TabIndex={TabIndex}&PlanningPeriod={PlanningPeriod}");
        }
        
        public async Task<PositionJobDetails> GetMasterJobDetails(int PlanCode)
        {
            return await _httpService.GetAsync<PositionJobDetails>($"{_baseURL}/GetMasterJobDetails?PlanCode={PlanCode}");
        }
        public async Task<ResponseEntity> SavePositionRequest(PositionRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, PositionRequest>($"{_baseURL}/SavePositionRequest", model);
        }
    }
}
