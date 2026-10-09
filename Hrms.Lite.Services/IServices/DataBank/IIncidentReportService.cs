using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IIncidentReportService
    {
        public Task<IncidentReport> GetList(int radiomode);
        public Task<IncidentReport> GetIncidentReportDetails(Guid EmployeeGI, int IncidentCode);

        public Task<ResponseEntity> Create(IncidentReport input);
    }
}
