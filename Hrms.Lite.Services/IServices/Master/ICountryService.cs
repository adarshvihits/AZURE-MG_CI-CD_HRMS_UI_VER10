using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICountryService
    {

        public Task<ResponseEntity> Create(Country input);
        public Task<Country>GetList(string TabIndex );
        public Task<Country> GetCountryDetails(Guid CountryGI,Guid LogGI, string Type );
        public Task<ResponseEntity> Delete(Country model);
        public Task<ResponseEntity> ApproveOrReject(Country model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);



    }
}
