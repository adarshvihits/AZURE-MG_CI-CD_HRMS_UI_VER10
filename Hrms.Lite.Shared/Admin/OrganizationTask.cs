using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
	public class OrganizationTask
	{
		public int TaskCode { get; set; }
		public string Task { get; set; }
		public DateTime? Assigneddate { get; set; }
		public DateTime? EDC { get; set; }
		public string Status { get; set; }
		public int StatusCode { get; set; }
		public string Mode { get; set; }
		public Dropdown Employee { get; set; }
		public Dropdown EmployeeSupervisor { get; set; }
		public List<OrganizationTask> OrganizationTaskList { get; set; }
	}

	
}
