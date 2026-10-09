using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.Services.TrainingModule
{
    public class TrainingDueListService : ITrainingDueListService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public TrainingDueListService(IServiceBase httpService)
        {
            _baseURL = "TrainingDueListRpt";
            _httpService = httpService;
        }
        public async Task<TrainingDueList> GetList(int trainingcode, Filter filter)
        {
            return await _httpService.PostAsync<TrainingDueList, Filter>($"{_baseURL}/List?trainingcode={trainingcode}",filter);
        }

        public async Task<DataSet> TrainingDueList_Excel(int trainingcode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TrainingDueList_Excel?trainingcode={trainingcode}", filter);

        }
    }
}
