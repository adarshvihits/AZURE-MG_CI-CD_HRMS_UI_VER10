using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface ITDSReturnFillingService
    {
        public Task<System.Data.DataSet> GetDetails( TDSReturnFilling model);
    }
}
