using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
 public   interface ITDSVariationReportService
    {
        public Task<ResponseEntity> TdsVariationReport(TDSVariationReport Model);
      

    }
}
