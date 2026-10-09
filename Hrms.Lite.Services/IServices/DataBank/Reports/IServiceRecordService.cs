using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
  public interface IServiceRecordService
    {
        public Task<ServiceRecord> GetServiceRecordList(Filter filter);
        public Task<Url> GetUrl();
    }
}
