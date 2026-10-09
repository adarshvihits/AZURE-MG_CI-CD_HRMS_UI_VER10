using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class InductionTrainingService: IInductionTrainingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public InductionTrainingService(IServiceBase httpService)
        {
            _baseURL = "InductionTraining";
            _httpService = httpService;
        }

        public async Task<InductionTraining> GetInductionList(Filter filter)
        {
            return await _httpService.PostAsync<InductionTraining, Filter>($"{_baseURL}/GetList", filter);
        }

        public async Task<ResponseEntity> InductionSave(InductionTraining model)
        {
            return await _httpService.PostAsync<ResponseEntity, InductionTraining>($"{_baseURL}/Save", model);
        }

        public async Task<InductionTraining> GetInductionTrainingList() 
        {
            return await _httpService.GetAsync<InductionTraining>($"{_baseURL}/GetInductionTrainingList");
        }
        public async Task<DataSet> ExportToExcel(Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", Filter);
        }
    }
}
