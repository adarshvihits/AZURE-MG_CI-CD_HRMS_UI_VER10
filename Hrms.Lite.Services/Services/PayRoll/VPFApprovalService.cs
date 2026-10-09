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
    public class VPFApprovalService: IVPFApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public VPFApprovalService(IServiceBase httpService)
        {
            _baseURL = "VPFApproval";
            _httpService = httpService;
        }
        public async Task<VPFApproval> GetList(string Tab,int FinancialYear)
        {
            return await _httpService.GetAsync<VPFApproval>($"{_baseURL}/GetList?Tab={Tab}&FinancialYear={FinancialYear}");
        }
        public async Task<ResponseEntity> Approve(VPFApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, VPFApproval>($"{_baseURL}/Approve", model);

        }
        public async Task<DataSet> GetList_Excel(string Tab, int FinancialYear)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?Tab={Tab}&FinancialYear={FinancialYear}");
        }
    }
}
