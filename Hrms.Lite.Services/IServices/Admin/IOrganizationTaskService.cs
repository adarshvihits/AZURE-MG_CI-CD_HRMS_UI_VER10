using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
	public interface IOrganizationTaskService
	{
		public Task<OrganizationTask> GetOrganizationTaskList();
		public Task<OrganizationTask> GetOrganizationTaskDetails(int TaskCode);
		public Task<ResponseEntity> Create(OrganizationTask Input);
		public Task<ResponseEntity> Delete(OrganizationTask Input);
		public Task<OrganizationTask> TaskUpdate(int TaskCode,int StatusCode);

		public Task<ResponseEntity> QuerySave(SoftwareSettings Input);
	}
}
