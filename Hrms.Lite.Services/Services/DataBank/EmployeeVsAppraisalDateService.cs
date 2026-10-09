using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsAppraisalDateService: IEmployeeVsAppraisalDateService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsAppraisalDateService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsAppraisalDate";
            _httpService = httpService;
        }

    
        async Task<EmployeeVsAppraisalDate> IEmployeeVsAppraisalDateService.GetList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsAppraisalDate, Filter>($"{_baseURL}/GetList", filter);
        }
        async Task<EmployeeVsAppraisalDate> IEmployeeVsAppraisalDateService.GetAppraisalDateDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeVsAppraisalDate>($"{_baseURL}/GetAppraisalDateDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> Create(EmployeeVsAppraisalDate model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsAppraisalDate>($"{_baseURL}/Create", model);
        }

        public async Task<DataSet> GetList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", filter);
        }
      
    }
}
