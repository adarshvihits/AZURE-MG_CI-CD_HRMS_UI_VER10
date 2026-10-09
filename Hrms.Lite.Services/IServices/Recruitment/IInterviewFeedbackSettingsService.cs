using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IInterviewFeedbackSettingsService
    {
        public Task<InterviewFeedbackSettings> GetInterviewFeedbackSettingsList();
        public Task<InterviewFeedbackSettings> GetInterviewFeedbackSettingsDetails(Guid FeedbackSetGI);
        public Task<ResponseEntity> Create(InterviewFeedbackSettings input);
    }
}
