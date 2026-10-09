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
    public class TrainingCompletionService: ITrainingCompletionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingCompletionService(IServiceBase httpService)
        {
            _baseURL = "TrainingCompletion";
            _httpService = httpService;
        }
        public async Task<InductionTraining> GetList(Filter filter)
        {
            return await _httpService.PostAsync<InductionTraining, Filter>($"{_baseURL}/GetList", filter);
        }

        public async Task<ResponseEntity> ApproveCompletion(InductionTraining model)
        {
            return await _httpService.PostAsync<ResponseEntity, InductionTraining>($"{_baseURL}/ApproveOrReject", model);
        }

        public async Task<InductionTraining> GetInductionTrainingDetails(int HeadCode, int EmployeeCode)
        {
            return await _httpService.GetAsync<InductionTraining>($"{_baseURL}/GetInductionTrainingDetails?HeadCode={HeadCode}&EmployeeCode={EmployeeCode}");
        }
    }
}
