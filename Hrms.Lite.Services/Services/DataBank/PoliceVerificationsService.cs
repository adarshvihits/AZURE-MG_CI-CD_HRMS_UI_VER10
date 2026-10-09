using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public  class PoliceVerificationsService:IPoliceVerificationsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PoliceVerificationsService(IServiceBase httpService)
        {
            _baseURL = "PoliceVerification";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> Delete(Guid VerificationGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?PoliceVerificationGI={VerificationGI}");
        }

        public async Task<PoliceVerification> GetList(string TabIndex, int Type,Filter filter)
        {
            return await _httpService.PostAsync<PoliceVerification,Filter>($"{_baseURL}/PoliceVerificationList?TabIndex={TabIndex}&&Type={Type}",filter);
        }

        public async Task<PoliceVerification> GetVerificationDetails(Guid VerificationGI, Guid EmpGI)
        {
            return await _httpService.GetAsync<PoliceVerification>($"{_baseURL}/GetPoliceVerificationDetails?PoliceVerificationGI={VerificationGI}&&EmployeeGI={EmpGI}");
        }

        public  Task<ResponseEntity> Save(PoliceVerification input)
        {
            return _httpService.PostAsync<ResponseEntity, PoliceVerification>($"{_baseURL}/Create", input);
        }


        public async Task<DataSet> GetPoliceVerificationList_Excel(string TabIndex, int Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPoliceVerificationList_Excel?TabIndex={TabIndex}&&Type={Type}", filter);
        }
        
    }
}
