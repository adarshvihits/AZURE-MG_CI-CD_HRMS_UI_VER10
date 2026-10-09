using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
   public class BankStatementService: IBankStatementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BankStatementService(IServiceBase httpService)
        {
            _baseURL = "BankStatement";
            _httpService = httpService;
        }

        public async Task<System.Data.DataSet> GetBankStatementDetails(BankStatement model)
        {
            return await _httpService.PostAsync<System.Data.DataSet, BankStatement>($"{_baseURL}/GetbankStatementDetails", model);
        }
      
        public async Task<ResponseEntity> SaveSFTPFileName(int ?MonthCode,string strfName)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/SaveSFTPFileName?MonthCode={MonthCode}&strfName={strfName}");
        }
        public async Task<ResponseEntity> CheckIfAlreadyPostedtoSFTP(int MonthCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/CheckIfAlreadyPostedtoSFTP?MonthCode={MonthCode}");
        }
      

    }
}
