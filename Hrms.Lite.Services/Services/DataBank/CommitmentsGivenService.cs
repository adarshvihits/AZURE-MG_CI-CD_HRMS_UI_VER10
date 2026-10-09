using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class CommitmentsGivenService : ICommitmentsGivenService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CommitmentsGivenService(IServiceBase httpService)
        {
            _baseURL = "CommitmentsGiven";
            _httpService = httpService;
        }

        public async Task<CommitmentsGiven> GetCommitmentsGivenList(int TabIndex, Filter model)
        {
            return await _httpService.PostAsync<CommitmentsGiven, Filter>($"{_baseURL}/GetCommitmentsGivenList?TabIndex={TabIndex}", model);
        }
        public async Task<DataSet> GetCommitmentsGiven_Excel(int TabIndex, Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCommitmentsGiven_Excel?TabIndex={TabIndex}", model);
        }
        public async Task<CommitmentsGiven> GetCommitmentsGivenPopUpDetails(int EmployeeCode)
        {
            return await _httpService.GetAsync<CommitmentsGiven>($"{_baseURL}/GetCommitmentsGivenPopUpDetails?EmployeeCode={EmployeeCode}");

        }
        public async Task<DataSet> GetCommitmentsGivenPopUp_Excel(int EmployeeCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetCommitmentsGivenPopUp_Excel?EmployeeCode={EmployeeCode}");
        }
        public Task<ResponseEntity> Create(CommitmentsGiven input)
        {
            return _httpService.PostAsync<ResponseEntity, CommitmentsGiven>($"{_baseURL}/Create", input);
        }
    }
}
