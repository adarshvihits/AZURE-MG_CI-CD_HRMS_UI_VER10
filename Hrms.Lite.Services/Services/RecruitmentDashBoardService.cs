using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services
{
  public class RecruitmentDashBoardService : IRecruitmentDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public RecruitmentDashBoardService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentDashBoard";
            _httpService = httpService;
        }

      public  async Task<RecruitmentDashBoard> GetBarDiagramDetails(int ClassificationType, int ClassificationCode, string DiagramType)
        {
            return await _httpService.GetAsync<RecruitmentDashBoard>($"{_baseURL}/GetBarDiagramDetails?ClassificationType={ClassificationType}&ClassificationCode={ClassificationCode}&DiagramType={DiagramType}");

        }

        public async Task<RecruitmentDashBoard> GetDashBoardDetails(int ClassificationType, int ClassificationCode)
        {
            return await _httpService.GetAsync<RecruitmentDashBoard>($"{_baseURL}/GetDashBoardDetails?ClassificationType={ClassificationType}&ClassificationCode={ClassificationCode}");
        }

        public async Task<RecruitmentDashBoard> GetVacancyNearlyDuesPopUpList()
        {
            return await _httpService.GetAsync<RecruitmentDashBoard>($"{_baseURL}/GetVacancyNearlyDuesPopUpList");

        }
    }
}
