using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.TDS;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Services.Services.TDS
{
   public class PreviousEmployerHRMSService: IPreviousEmployerHRMSService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PreviousEmployerHRMSService(IServiceBase httpService)
        {

            _baseURL = "PreviousEmployer";
            _httpService = httpService;
        }

        public async Task<PreviousEmployer> GetList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<PreviousEmployer, Filter>($"{_baseURL}/GetPreviousEmployerEmpList?AssessmentYear={AssessmentYear}", filter);
        }
        public async Task<ResponseEntity> SavePreviousEmployer(PreviousEmployer model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreviousEmployer>($"{_baseURL}/Save", model);
        }
        public async Task<PreviousEmployer> GetPreviousEmployerDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PreviousEmployer>($"{_baseURL}/GetPreviousEmployerDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> DeletePreviousEmployer(Guid EmployeeGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePreviousEmployer?EmployeeGI={EmployeeGI}");
        }
      
    }
}
