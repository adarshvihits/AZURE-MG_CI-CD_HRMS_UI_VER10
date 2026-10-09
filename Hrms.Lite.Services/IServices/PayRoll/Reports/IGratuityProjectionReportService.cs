using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IGratuityProjectionReportService
    {
        public Task<GratuitySettings> GratuityProjectionList(int Type,Filter filter);

        public Task<DataSet> GetGratuityProjection_Excel(int Type, Filter filter);
    }
}
