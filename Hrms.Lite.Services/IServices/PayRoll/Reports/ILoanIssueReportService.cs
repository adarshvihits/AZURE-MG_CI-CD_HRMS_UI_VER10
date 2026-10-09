using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
	public interface ILoanIssueReportService
	{
		public Task<LoanIssueReport> GetLoanIssueReport(string FromDate, string ToDate, int type, Filter filter);
		Task<DataSet> GetLoanIssueReport_Excel(string FromDate, string ToDate, int type, Filter filter);
	}
}
