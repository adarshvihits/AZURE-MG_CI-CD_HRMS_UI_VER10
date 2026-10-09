using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
   public  interface IRecruitmentDashBoardService
    {
        Task<RecruitmentDashBoard> GetDashBoardDetails( int ClassificationType, int ClassificationCode);
        Task<RecruitmentDashBoard> GetBarDiagramDetails( int ClassificationType, int ClassificationCode, string DiagramType);
        Task<RecruitmentDashBoard> GetVacancyNearlyDuesPopUpList();
    }
}
