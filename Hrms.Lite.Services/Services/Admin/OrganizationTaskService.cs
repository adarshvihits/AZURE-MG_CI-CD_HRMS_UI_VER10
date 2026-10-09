using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Admin
{
	public class OrganizationTaskService: IOrganizationTaskService
	{
        private readonly string _baseURL;


        private readonly IServiceBase _httpService;
        public OrganizationTaskService(IServiceBase httpService)
        {
            _baseURL = "OrganizationTask";
            _httpService = httpService;
        }

        public async Task<OrganizationTask> GetOrganizationTaskList()
        {
            return await _httpService.GetAsync<OrganizationTask>($"{_baseURL}/GetOrganizationTaskList?");
        }
        public Task<OrganizationTask> GetOrganizationTaskDetails(int TaskCode)
        {
            return _httpService.GetAsync<OrganizationTask>($"{_baseURL}/GetOrganizationTaskDetails?TaskCode={TaskCode}");
        }
        public Task<ResponseEntity> Create(OrganizationTask Input)
        {
            return _httpService.PostAsync<ResponseEntity, OrganizationTask>($"{_baseURL}/Create", Input);
        }
        public Task<ResponseEntity> Delete(OrganizationTask Input)
        {
            return _httpService.PostAsync<ResponseEntity, OrganizationTask>($"{_baseURL}/Delete", Input);
        }
        public Task<OrganizationTask>TaskUpdate(int TaskCode, int StatusCode)
        {
            return _httpService.GetAsync<OrganizationTask>($"{_baseURL}/TaskUpdate?TaskCode={TaskCode}&StatusCode={StatusCode}");
        }

        public Task<ResponseEntity> QuerySave(SoftwareSettings Input)
        {
            return _httpService.PostAsync<ResponseEntity, SoftwareSettings>($"{_baseURL}/QuerySave", Input);
        }
    }
}
