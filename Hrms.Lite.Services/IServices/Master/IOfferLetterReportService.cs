using Hrms.Lite.Services.Services.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IOfferLetterReportService
    {
        public Task<Url> GetUrl();
        public Task<Url> GetOfferLetter(string Type);
    }
}
