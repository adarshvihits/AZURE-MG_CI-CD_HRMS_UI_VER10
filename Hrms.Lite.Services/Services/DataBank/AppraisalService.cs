using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class AppraisalService: IAppraisalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AppraisalService(IServiceBase httpService)
        {
            _baseURL = "Appraisal";
            _httpService = httpService;
        }

        async Task<ResponseEntity> IAppraisalService.AppraisalSave(Appraisal model)
        {
            return await _httpService.PostAsync<ResponseEntity, Appraisal>($"{_baseURL}/AppraisalSave", model);
        }
        async Task<Appraisal> IAppraisalService.GetAppraisalList(int Tabindex, int? SearchId, int? AppraisalTypeCode, Filter filter)
        {
            return await _httpService.PostAsync<Appraisal, Filter>($"{_baseURL}/GetAppraisalList?Tabindex={Tabindex}&SearchId={SearchId}&AppraisalTypeCode={AppraisalTypeCode}", filter);
        }
        async Task<IList<AppraisalTabs>> IAppraisalService.GetAppraisalTabs()
        {
            return await _httpService.GetAsync<IList<AppraisalTabs>>($"{_baseURL}/GetAppraisalTabs");
        }
        async Task<Appraisal> IAppraisalService.GetAppraisalAssesmentMode(int AppraisalTypeCode)
        {
            return await _httpService.GetAsync<Appraisal>($"{_baseURL}/GetAppraisalAssesmentMode?AppraisalTypeCode={AppraisalTypeCode}");
        }
        async Task<IList<AppraisalLog>> IAppraisalService.GetAppraisalLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<AppraisalLog>>($"{_baseURL}/GetAppraisalLogList?EmployeeGI={EmployeeGI}");
        }
        async Task<AppraisalRequest> IAppraisalService.GetAppraisalLogDetails(Guid AppraisalTrnGI)
        {
            return await _httpService.GetAsync<AppraisalRequest>($"{_baseURL}/GetAppraisalDetails?AppraisalTrnGI={AppraisalTrnGI}");
        }
        async Task<AppraisalRequest> IAppraisalService.GetAppraisalDetails(Guid AppraisalTrnGI)
        {
            return await _httpService.GetAsync<AppraisalRequest>($"{_baseURL}/GetAppraisalDetails?AppraisalTrnGI={AppraisalTrnGI}");
        }
        public async Task<ResponseEntity> AppraisalDelete(Guid AppraisalTrnGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/AppraisalDelete?AppraisalTrnGI={AppraisalTrnGI}");
        }
        async Task<Appraisal> IAppraisalService.GetAppraisalEvaluationPeriod()
        {
            return await _httpService.GetAsync<Appraisal>($"{_baseURL}/GetAppraisalDefaultEvaluationPeriod");
        }
    }
}
