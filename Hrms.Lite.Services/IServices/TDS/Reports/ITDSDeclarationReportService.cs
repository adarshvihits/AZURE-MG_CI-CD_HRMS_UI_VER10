using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface ITDSDeclarationReportService
    {
        public Task<TDSDeclarationReport> GetTDSDeclarationReport(int AssessmentYear);
        public Task<DataSet> GetTDSDetailedReport( int CategoryCode, int AssessmentYear);
        public Task<DataSet> GetTDSReport_Export(Filter filter, int AssessmentYear);

    }
}
