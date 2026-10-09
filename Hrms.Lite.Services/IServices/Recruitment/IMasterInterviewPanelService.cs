using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IMasterInterviewPanelService
    {
        public Task<MasterInterviewPanel> GetMasterInterviewPanelList();
        public Task<MasterInterviewPanel> GetMasterInterviewPanelDetails(Guid PanelGI);
        public Task<ResponseEntity> Create(MasterInterviewPanel input);
    }
}
