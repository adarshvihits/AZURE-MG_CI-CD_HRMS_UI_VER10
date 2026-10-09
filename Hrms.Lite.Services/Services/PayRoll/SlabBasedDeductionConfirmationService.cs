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
    public class SlabBasedDeductionConfirmationService: ISlabBasedDeductionConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SlabBasedDeductionConfirmationService(IServiceBase httpService)
        {
            _baseURL = "SlabBasedDeductionConfirmation";
            _httpService = httpService;
        }

        public async Task<SlabDeductionConfirmation> DedConfirmationList(int MonthCode, int DeductionCode,int ConfirmStatus,Filter filter1)
        {
            return await _httpService.PostAsync<SlabDeductionConfirmation, Filter>($"{_baseURL}/GetList?MonthCode={MonthCode}&DeductionCode={DeductionCode}&ConfirmStatus={ConfirmStatus}", filter1);

        }

        public async Task<ResponseEntity> EmployeeWiseSave(SlabDeductionConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, SlabDeductionConfirmation>($"{_baseURL}/EmployeeWisSave", model);
        }

        public async Task<SlabDeductionConfirmation> GetList(int MonthCode, int DeductionCode, int ConfirmStatus, Filter filter1)
        {
            return await _httpService.PostAsync<SlabDeductionConfirmation, Filter>($"{_baseURL}/GetList?MonthCode={MonthCode}&DeductionCode={DeductionCode}&ConfirmStatus={ConfirmStatus}", filter1);
        }

        public async Task<ResponseEntity> ConfirmAll(SlabDeductionConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, SlabDeductionConfirmation>($"{_baseURL}/ConfirmAll", model);
        }
    }
}
