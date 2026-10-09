using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class JobTransitionTypeService : IJobTransitionTypeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JobTransitionTypeService(IServiceBase httpService)
        {
            _baseURL = "JobTransitionType";
            _httpService = httpService;
        }

        public async Task<JobTransitionType> GetList()
        {
            return await _httpService.GetAsync<JobTransitionType>($"{_baseURL}/List");
        }

        public async Task<ResponseEntity> Save(JobTransitionType model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobTransitionType>($"{_baseURL}/Save", model);
        }
        public async Task<DataSet> GetJobTransitionType_Excel(int groupCompanyCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetJobTransitionType_Excel?groupCompanycode={groupCompanyCode}");
        }
    }
}
