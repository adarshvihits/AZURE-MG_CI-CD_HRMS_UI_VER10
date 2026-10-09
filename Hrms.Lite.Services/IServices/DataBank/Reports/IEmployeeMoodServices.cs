using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
   public interface IEmployeeMoodServices
    {
        public Task<EmployeeMood> GetEmployeeMoodDetails(string Date,int moodcode,Filter filter);

        public Task<DataSet> EmployeeMoodList_Excel(Filter filter, string Date, int moodcode);

        public Task<EmployeeMood> GetApplyFilter(string Date, int moodcode, Filter filter);
    }
}
