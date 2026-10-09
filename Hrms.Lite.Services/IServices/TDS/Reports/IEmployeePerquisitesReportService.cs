using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
    public interface IEmployeePerquisitesReportService
    {
        public Task<List<EmployeePerquisitesReport>> GetList(int AssessmentYear, int Month, int PerquisiteNature, Filter filter);
        public Task<DataSet> GetList_Excel(int AssessmentYear, int Month, int PerquisiteNature, Filter filter);
    }
}
