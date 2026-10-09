using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmpConfirmationService: IEmpConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmpConfirmationService(IServiceBase httpService)
        {
            _baseURL = "EmpConfirmation";
            _httpService = httpService;
        }
        public async Task<EmpConfirmation> GetList()
        {
            return await _httpService.GetAsync<EmpConfirmation>($"{_baseURL}/GetList");
        }
        public async Task<DataSet> EmpConfirmation_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/EmpConfirmation_Excel");
        }
    }
}
