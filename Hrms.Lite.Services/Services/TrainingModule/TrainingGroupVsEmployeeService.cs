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
    public class TrainingGroupVsEmployeeService : ITrainingGroupVsEmployeeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingGroupVsEmployeeService(IServiceBase httpService)
        {
            _baseURL = "TrainingGroupVsEmployee";
            _httpService = httpService;
        }
        public async Task<TrainingGroupVsEmployee> GetEmployeeList( Filter filter)
        {
            return await _httpService.PostAsync<TrainingGroupVsEmployee, Filter>($"{_baseURL}/GetEmployeeList", filter);
        }
        public async Task<ResponseEntity> Save(TrainingGroupVsEmployee input)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingGroupVsEmployee>($"{_baseURL}/Create", input);
        }
        public async Task<DataSet> GetEmployeeList_Excel( Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel", filter);
        }
        public async Task<ResponseEntity> TrainingGroupVsEmployeeBatchUpdate(Filter filter, int TrainingGroupCode)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/TrainingGroupVsEmployeeBatchUpdate?TrainingGroupCode={TrainingGroupCode}", filter);
        }
    }
}
