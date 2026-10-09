using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface  ISalaryProjectionReportService
    {
        Task<DataSet> GetList(int? AmountType, int? Type, string Amount, string Allowance, SalaryProjectionReport model);
        //public Task<DataSet> SalaryProj_Excel();

    }
}
