using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
	public class LoanIssueReport
	{
		public Guid LoanIssueGI { get; set; }
		public int LoanIssueCode { get; set; }
		public int EmpCode { get; set; }
		public string EmployeeID { get; set; }
		public string EmployeeName { get; set; }
		public Guid EmployeeGI { get; set; }
		public EmployeeHeader EmployeeHeader { get; set; }
		public string Designation { get; set; }
		public string Department { get; set; }
		public string Location { get; set; }
		public string Division { get; set; }
		public DateTime LoanIssueDate { get; set; }
		public int TotalTenure { get; set; }
		public int PrincipleAmount { get; set; }
		public int TopUpAmount { get; set; }
		public int AdjustmentAmount { get; set; }
		public int TotalMonthlyEMI { get; set; }
		public int AlreadyPaidAmount { get; set; }
		public int TotalBalanceAmount { get; set; }
		public int RemainingTenure { get; set; }
		public string EmployeeStatus { get; set; }
		public string LoanSettingsName { get; set; }
		public string LoanStatus { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public Filter filter { get; set; }
		public LoanType Loans { get; set; }
		public List<LoanIssueReport> LoanIssueReports { get; set; }
		public int totalRecords { get; set; }
		public int active { get; set; }
	}
}
