using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class TrainingVsSubjectService : ITrainingVsSubjectService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingVsSubjectService(IServiceBase httpService)
        {
            _baseURL = "TrainingVsSubject";
            _httpService = httpService;
        }
        public async Task<TrainingVsSubject> GetList(int TrainingCode)
        {
            return await _httpService.GetAsync<TrainingVsSubject>($"{_baseURL}/List?TrainingCode={TrainingCode}");
        }
        public Task<ResponseEntity> SubjectSave(TrainingVsSubject input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingVsSubject>($"{_baseURL}/SubjectSave", input);
        }
    }
}
