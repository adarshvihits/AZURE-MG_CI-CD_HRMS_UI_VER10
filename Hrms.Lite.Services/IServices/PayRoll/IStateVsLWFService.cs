using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IStateVsLWFService
    { 
    public Task<LWFSettings> GetList(int DeductionCode);
        public Task<LWFSettings> GetPopUpDetails(int StateCode,int DeductionCode);
        public Task<ResponseEntity> Save(LWFSettings model);
    }
}
