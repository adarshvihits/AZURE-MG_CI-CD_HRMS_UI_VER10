using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IRecruitmentMatrixService
    {
        Task<RecruitmentMatrix> RecruitmentMatrixList(int TemplateList, Filter model);
        Task<ResponseEntity> RecruitmentMatrixSave(RecruitmentMatrix model);
        Task<ResponseEntity> RecruitmentMatrixUpdate(RecruitmentMatrix model,int Template);
        Task<ResponseEntity> RecruitmentMatrixDelete(Guid RecruitmentMatrixGI);
        Task<ResponseEntity> RecruitmentMatrixLevelDelete(Guid RecruitmentMatrixGI, int Level);
        Task<RecruitmentMatrix> GetRecruitmentMatrixDetails(Guid RecruitmentMatrixGI);
        Task<RecruitmentMatrix> GetRecruitmentMatrixEditDetails(int TemplateList);
        Task<DataSet> GetList_Excel(int WorkFlow, Filter model);
        Task<RecruitmentMatrix> GetRoleList(RecruitmentMatrix model);
        Task<RecruitmentMatrix> GetEditRoleList(int? NoOfLevels, Guid RecruitmentMatrixGI);
        Task<List<Dropdown>> GetAuthorityEntityDropdown(RecruitmentMatrix model);
    }
}
