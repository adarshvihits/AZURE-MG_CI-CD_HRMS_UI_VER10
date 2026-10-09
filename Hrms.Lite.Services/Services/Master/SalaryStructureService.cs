using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class SalaryStructureService : ISalaryStructureService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public SalaryStructureService(IServiceBase httpService)
        {
            _baseURL = "SalaryStructure";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        public async Task<SalaryStructureMaster> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<SalaryStructureMaster>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<SalaryStructureMaster> GetResultList(Guid SalaryStructureGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<SalaryStructureMaster>($"{_baseURL}/SalaryStructureDetails?SalaryStructureGI={SalaryStructureGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> Save(SalaryStructureMaster model)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryStructureMaster>($"{_baseURL}/SaveSalaryStructure", model);
        }
        public Task<ResponseEntity> ApproveOrReject(SalaryStructureMaster model)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryStructureMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Guid gi)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?gi={gi}");

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
