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
   public class MedicalCheckupService : IMedicalCheckupService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MedicalCheckupService(IServiceBase httpService)
        {
            _baseURL = "MedicalCheckup";
            _httpService = httpService;
        }
        public async Task<MedicalCheckup> GetMedicalCheckupList(int Type, string TabIndex,Filter filter)
        {
            return await _httpService.PostAsync<MedicalCheckup,Filter>($"{_baseURL}/MedicalCheckupList?TabIndex={TabIndex}&Type={Type}",filter);
        }


        public async Task<DataSet> GetMedicalCheckupList_Excel(string TabIndex, int Type, Filter filter)

        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetMedicalCheckupList_Excel?Type={Type}&TabIndex={TabIndex}", filter);
        }


        public Task<ResponseEntity> Save(MedicalCheckup input)
        {
            return _httpService.PostAsync<ResponseEntity, MedicalCheckup>($"{_baseURL}/Create", input);
        }

        public Task<MedicalCheckup> GetMedicalCheckUpDetails(Guid EmpGI, Guid MedicalGI)
        {
            return _httpService.GetAsync<MedicalCheckup>($"{_baseURL}/MedicalCheckupDetails?MedicalCheckupGI={MedicalGI}&EmployeeGI={EmpGI}");
        }
        async Task<ResponseEntity> IMedicalCheckupService.Delete(Guid MedicalGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?MedicalCheckupGI={MedicalGI}");
        }
    }
}
