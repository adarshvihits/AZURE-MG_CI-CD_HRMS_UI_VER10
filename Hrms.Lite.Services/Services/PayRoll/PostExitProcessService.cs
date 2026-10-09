using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class PostExitProcessService: IPostExitProcessService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PostExitProcessService(IServiceBase httpService)
        {
            _baseURL = "PostExitProcess";
            _httpService = httpService;
        }
        public async Task<PostExitProcess> GetList(string fromdate, string todate, Filter filter)
        {
            return await _httpService.PostAsync<PostExitProcess, Filter>($"{_baseURL}/GetPostExitProcessList?fromdate={fromdate}&ToDate={todate}", filter);
        }

        public async Task<DataSet> GetPostExitProcessList_Excel(string fromdate, string todate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPostExitProcessList_Excel?fromdate={fromdate}&ToDate={todate}", filter);
        }
        public async Task<PostExitProcess> GetPostExitDetails(Guid EmpGI)
        {
            return await _httpService.GetAsync<PostExitProcess>($"{_baseURL}/GetPostExitDetails?EmployeeGI={EmpGI}");
        }
        public async Task<ResponseEntity> Save(PostExitProcess model)
        {
            return await _httpService.PostAsync<ResponseEntity, PostExitProcess>($"{_baseURL}/PostExitProcessSave", model);
        }
    }
}
