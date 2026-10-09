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
   public class BranchGradingService:IBranchGradingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BranchGradingService(IServiceBase httpService)
        {
            _baseURL = "BranchGrading";
            _httpService = httpService;
        }

        public Task<BranchGrading> GetMainList(int DesignationCode, int RegionCode, int GradeCode)
        {
            return  _httpService.GetAsync<BranchGrading>($"{_baseURL}/GetBranchGradingList?DesignationCode={DesignationCode}&RegionCode={RegionCode}&GradeCode={GradeCode}");
        }


        public Task<BranchGrading> GetBranchGradingPopUp(int DesignationCode, int RegionCode, int GradeCode,int LocationCode)
        {
            return _httpService.GetAsync<BranchGrading>($"{_baseURL}/GetBranchGradingDetails?DesignationCode={DesignationCode}&RegionCode={RegionCode}&GradeCode={GradeCode}&LocationCode={LocationCode}");
        }

        public Task<BranchGrading> GetGradeList(int GradeCode)
        {
            return _httpService.GetAsync<BranchGrading>($"{_baseURL}/GetGradeList?GradeCode={GradeCode}");
        }

        public Task<ResponseEntity> SaveGradeList(BranchGrading model)
        {
            return _httpService.PostAsync<ResponseEntity, BranchGrading>($"{_baseURL}/SaveGradeList",model);
        }

        public Task<ResponseEntity> ChangeBranchGrade(BranchGrading model)
        {
            return _httpService.PostAsync<ResponseEntity, BranchGrading>($"{_baseURL}/ChangeBranchGrade", model);
        }

        public Task<BranchGrading> GetGpPopUpDetails(int DesignationCode, int RegionCode, int GradeCode, int LocationCode, string Type)
        {
            return _httpService.GetAsync<BranchGrading>($"{_baseURL}/GetEmployeeDetails?DesignationCode={DesignationCode}&RegionCode={RegionCode}&GradeCode={GradeCode}&LocationCode={LocationCode}&Type={Type}");
        }

        //public Task<BranchGrading> GetNegPopUpDetails(int DesignationCode, int RegionCode, int GradeCode, int LocationCode, string Type)
        //{
        //    return _httpService.GetAsync<BranchGrading>($"{_baseURL}/GetEmployeeDetails?DesignationCode={DesignationCode}&RegionCode={RegionCode}&GradeCode={GradeCode}&LocationCode={LocationCode}&Type={Type}");
        //}
    }
}
