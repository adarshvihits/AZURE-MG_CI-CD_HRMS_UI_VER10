using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
   public  class TDSConfirmationService:ITDSConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSConfirmationService(IServiceBase httpService)
        {
            _baseURL = "TDSConfirmation";
            _httpService = httpService;
        }

        public async Task<TDSConfirmation> GetTDSConfirmationList(int Month, int Deduction,Filter filter)
        {
            return await _httpService.PostAsync<TDSConfirmation,Filter>($"{_baseURL}/TDSConfirmationList?Month={Month}&Deduction={Deduction}",filter);
        }
        public async Task<ResponseEntity> EmployeeWiseTDSConfirmationSave(TDSConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, TDSConfirmation>($"{_baseURL}/EmployeeWiseTDSConfirmationSave", model);
        }


        public async Task<DataSet> TDSConfirmationList_Excel(int Month, int Deduction, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TDSConfirmationList_Excel?Month={Month}&Deduction={Deduction}", filter);
        }

        public async Task<ResponseEntity<List<TDSConfirmation>>> ImportFromExcel(TDSConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<TDSConfirmation>>, TDSConfirmation>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
