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
    public class TrainingService : ITrainingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingService(IServiceBase httpService)
        {
            _baseURL = "Training";
            _httpService = httpService;
        }
        async Task<IList<TrainingListTabs>> ITrainingService.GetTrainingTabs()
        {
            return await _httpService.GetAsync<IList<TrainingListTabs>>($"{_baseURL}/GetTrainingTabs");
        }
        async Task<Training> ITrainingService.GetTrainingList(int Tabindex, int SearchId, int? TrainingTabCode, Filter Filter)
        {
            return await _httpService.PostAsync<Training, Filter>($"{_baseURL}/GetTrainingList?Tabindex={Tabindex}&SearchId={SearchId}&TrainingCode={TrainingTabCode}", Filter);
        }
        async Task<Training> ITrainingService.GetTrainingDetails(Guid EmployeeTrainingGI)
        {
            return await _httpService.GetAsync<Training>($"{_baseURL}/GetTrainingDetails?EmployeeTrainingGI={EmployeeTrainingGI}");
        }
        async Task<Training> ITrainingService.GetTrainingTypeDetails(int TrainingCode)
        {
            return await _httpService.GetAsync<Training>($"{_baseURL}/GetTrainingTypeDetails?TrainingCode={TrainingCode}");
        }
        async Task<IList<TrainingLog>> ITrainingService.GetTrainingLogList(Guid EmployeeGI, int SearchId)
        {
            return await _httpService.GetAsync<IList<TrainingLog>>($"{_baseURL}/GetTrainingLogList?EmployeeGI={EmployeeGI}&SEARCHID={SearchId}");
        }
        async Task<ResponseEntity> ITrainingService.TrainingSave(Training model)
        {
            return await _httpService.PostAsync<ResponseEntity, Training>($"{_baseURL}/TrainingSave", model);
        }
        async Task<ResponseEntity> ITrainingService.TrainingDetailsDelete(Guid EmployeeTrainingGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/{EmployeeTrainingGI}");
        }
    }
}
