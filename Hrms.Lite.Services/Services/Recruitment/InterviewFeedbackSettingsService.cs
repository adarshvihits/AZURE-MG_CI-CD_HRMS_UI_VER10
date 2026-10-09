using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class InterviewFeedbackSettingsService : IInterviewFeedbackSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public InterviewFeedbackSettingsService(IServiceBase httpService)
        {
            _baseURL = "InterviewFeedbackSettings";
            _httpService = httpService;
        }

        public Task<InterviewFeedbackSettings> GetInterviewFeedbackSettingsList()
        {
            return _httpService.GetAsync<InterviewFeedbackSettings>($"{_baseURL}/GetInterviewFeedbackSettingsList");
        }

        public Task<InterviewFeedbackSettings> GetInterviewFeedbackSettingsDetails(Guid FeedbackSetGI)
        {
            return _httpService.GetAsync<InterviewFeedbackSettings>($"{_baseURL}/GetInterviewFeedbackSettingsDetails?FeedbackSetGI={FeedbackSetGI}");
        }

        public Task<ResponseEntity> Create(InterviewFeedbackSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, InterviewFeedbackSettings>($"{_baseURL}/Create", input);
        }
    }
}
