using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class MasterInterviewPanelService : IMasterInterviewPanelService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MasterInterviewPanelService(IServiceBase httpService)
        {
            _baseURL = "MasterInterviewPanel";
            _httpService = httpService;
        }

        public Task<MasterInterviewPanel> GetMasterInterviewPanelList()
        {
            return _httpService.GetAsync<MasterInterviewPanel>($"{_baseURL}/GetMasterInterviewPanelList");
        }

        public Task<MasterInterviewPanel> GetMasterInterviewPanelDetails(Guid PanelGI)
        {
            return _httpService.GetAsync<MasterInterviewPanel>($"{_baseURL}/GetMasterInterviewPanelDetails?PanelGI={PanelGI}");
        }

        public Task<ResponseEntity> Create(MasterInterviewPanel input)
        {
            return _httpService.PostAsync<ResponseEntity, MasterInterviewPanel>($"{_baseURL}/Create", input);
        }
    }
}
