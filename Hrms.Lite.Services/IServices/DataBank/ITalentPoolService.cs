using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ITalentPoolService
    {
        public Task<TalentPool> GetClassificationDetails();
        public Task<TalentPool> GetTalentPoolDetails(TalentPool model);
        Task<DataSet> GetTalentPoolDetails_Excel(TalentPool model);
        public Task<TalentPool> GetEmployeeTalentList(string TalentType, int Code);
        public Task<TalentPool> GetSkillGraph();
    }
}
