
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IHolidaySerivce
    {

        public Task<Holiday> GetList(string TabIndex, int yearcode);
        public Task<ResponseEntity> Create(Holiday input);
        public Task<Holiday> GetHolidayDetails(Guid HolidayGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Holiday model);
        public Task<ResponseEntity> Delete(Holiday model);
        public Task<DateAndMonth> GetYearCode();
    }
}
