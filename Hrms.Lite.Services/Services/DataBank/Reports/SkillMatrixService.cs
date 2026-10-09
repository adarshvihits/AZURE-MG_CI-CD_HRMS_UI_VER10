using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SkillMatrixService : ISkillMatrixService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public SkillMatrixService(IServiceBase httpService)
        {
            _baseURL = "SkillMatrix";
           
            _httpService = httpService;
        }
        async Task<SkillMatrix> ISkillMatrixService.GetSkillMatrix(Filter filter)
        {
            return await _httpService.PostAsync<SkillMatrix, Filter>($"{_baseURL}/GetSkillMatrix", filter);
        }
        async Task<DataSet> ISkillMatrixService.GetSkillMatrixReport(Filter filter,int type,string skills)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSkillMatrixReport?type="+type+"&skills="+skills, filter);
        }
        async Task<DataSet> ISkillMatrixService.GetSkillMatrixHeader(Filter filter,int type, string skills)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSkillMatrixHeader?type="+type + "&skills=" + skills, filter);
        }

        async Task<DataSet> ISkillMatrixService.GetSkillMatrixEmployeeCount(Filter filter, int type, string mode)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSkillMatrixEmployeeCount?type=" + type + "&mode=" + mode, filter);
        }


        async Task<SkillMatrix> ISkillMatrixService.GetSkillMatrixEmployeeDetails(Filter filter, int skill, string rating)
        {
            return await _httpService.PostAsync<SkillMatrix, Filter>($"{_baseURL}/GetSkillMatrixEmployeeDetails?skill=" + skill + "&rating=" + rating, filter);
        }

        async Task<DataSet> ISkillMatrixService.GetSkillNames()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetSkillNames");
        }

        async Task<SkillMatrix> ISkillMatrixService.GetSkills()
        {
            return await _httpService.GetAsync<SkillMatrix>($"{_baseURL}/GetSkills");
        }

    }
}
