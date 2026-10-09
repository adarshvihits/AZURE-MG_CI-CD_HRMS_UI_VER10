using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
	public interface IExitCheckListReportService
	{
		public Task<ExitCheckListReport> GetExitCheckListReport(string FromDate, string ToDate, Filter filter);
		Task<DataSet> GetExitCheckListReport_Excel(string FromDate, string ToDate, Filter filter);
		Task<ExitCheckListReport> GetExitCheckListDetails(int EmployeeCode);
		Task<DataSet> GetExitCheckListPopUp_Excel(int EmployeeCode);
	}
}
