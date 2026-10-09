using Hrms.Lite.Services.Services.TDS.Reports;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
 public interface ITDS24QService
    {
        public Task<System.Data.DataSet> GetDetails(TDS24Q model);
    }
}
