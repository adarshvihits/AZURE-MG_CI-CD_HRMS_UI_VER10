using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IBranchGradingService
    {
        public Task<BranchGrading> GetMainList(int DesignationCode, int RegionCode,int GradeCode);
        public Task<BranchGrading> GetGradeList(int GradeCode);
        public Task<BranchGrading> GetBranchGradingPopUp(int DesignationCode, int RegionCode, int GradeCode,int LocationCode);
        public Task<ResponseEntity> SaveGradeList(BranchGrading model);
        public Task<ResponseEntity> ChangeBranchGrade(BranchGrading model);
        public Task<BranchGrading> GetGpPopUpDetails(int DesignationCode, int RegionCode, int GradeCode, int LocationCode,string Type);
        //public Task<BranchGrading> GetNegPopUpDetails(int DesignationCode, int RegionCode, int GradeCode, int LocationCode, string Type);
    }
}
