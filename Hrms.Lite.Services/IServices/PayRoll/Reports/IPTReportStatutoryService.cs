using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IPTReportStatutoryService
    {
        public Task<PTReportStatutory> GetList(int Month, Filter filter);
        public Task<PTReportStatutory> GetPTReportStatutory(int Month);

    }





}
