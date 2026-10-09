using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IVacancyCancellationReportService
    {
        public Task<VacancyCancellation> GetList();
    }
}
