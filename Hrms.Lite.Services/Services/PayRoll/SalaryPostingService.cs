using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
  public  class SalaryPostingService: ISalaryPostingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryPostingService(IServiceBase httpService)
        {
            _baseURL = "SalaryPosting";
            _httpService = httpService;
        }
    
        public async Task<SalaryPosting> SalaryPostingValidations(int SalaryPeriodCode, int PayrollGroup)
        {
            return await _httpService.GetAsync<SalaryPosting>($"{_baseURL}/SalaryPostingValidations?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroup={PayrollGroup}");
        }

        public async Task<SalaryPosting> GetEmployeeList(int Month, string Type, string desc, int Code)
        {
            return await _httpService.GetAsync<SalaryPosting>($"{_baseURL}/GetSalaryPosting_EmployeeList?SalaryPeriod={Month}&Type={Type}&Description={desc}&Code={Code}");

        }
        public async Task<SalaryPosting> SalaryPostingValidationsPopUp(int SalaryPeriodCode, int PayrollGroup,int DescriptionCode , string Description)
        {
            return await _httpService.GetAsync<SalaryPosting>($"{_baseURL}/SalaryPostingValidationsPopUp?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroup={PayrollGroup}&DescriptionCode={DescriptionCode}&Description={Description}");
        }
        public async Task<ResponseEntity> SalaryPosting(SalaryPosting Model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryPosting>($"{_baseURL}/SalaryPosting", Model);
        }
        public async Task<ResponseEntity<SalaryPosting>> MonthlySalaryPostingValidation(int SalaryPeriodCode, int PayrollGroupCode)
        {
            return await _httpService.GetAsync<ResponseEntity<SalaryPosting>>($"{_baseURL}/MonthlySalaryPostingValidation?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroupCode={PayrollGroupCode}");
        }

        //********************Individual Salary Posting******************************
        public async Task<SalaryPosting> GetIndividualSalaryPostingList(int Month, int PayrollGrp, Filter filter)
        {
            return await _httpService.PostAsync<SalaryPosting, Filter>($"{_baseURL}/GetIndividualSalaryPostingList?Month={Month}&PayrollGrp={PayrollGrp}", filter);
        }
        public async Task<DataSet> GetIndividualSalaryPostingList_Excel(int Month, int PayrollGrp, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetIndividualSalaryPostingList_Excel?Month={Month}&PayrollGrp={PayrollGrp}", filter);
        }
        public async Task<NetPaySplitUp> GetIndividualPostingSplitUpDetails(Guid EmployeeGI, int SalaryPeriod, int PayrollGroup)
        {
            return await _httpService.GetAsync<NetPaySplitUp>($"{_baseURL}/GetIndividualPostingSplitUpDetails?EmployeeGI={EmployeeGI}&SalaryPeriod={SalaryPeriod}&PayrollGroup={PayrollGroup}");
        }
        public async Task<ResponseEntity> IndividualSalaryPosting(int SalaryPeriodCode, int EmployeeCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/IndividualSalaryPosting?SalaryPeriodCode={SalaryPeriodCode}&EmployeeCode={EmployeeCode}");
        }

    }
}
