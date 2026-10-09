using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IFullAndFinalKendoReportService
    {
        public Task<Url> GetUrl();
    }
}
