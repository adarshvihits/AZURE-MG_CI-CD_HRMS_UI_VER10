using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface ISalaryPostingService
    {
       
        public Task<SalaryPosting> SalaryPostingValidations(int SalaryPeriodCode, int PayrollGroup);
        public Task<SalaryPosting> SalaryPostingValidationsPopUp(int SalaryPeriodCode, int PayrollGroup,int DescriptionCode, string Description);
        public Task<SalaryPosting> GetEmployeeList(int Month, string Type, string desc, int Code);
        public Task<ResponseEntity> SalaryPosting(SalaryPosting Model);
        public Task<ResponseEntity<SalaryPosting>> MonthlySalaryPostingValidation(int SalaryPeriodCode, int PayrollGroupCode);

        //******************Individual Salary Postiong********************************
        public Task<SalaryPosting> GetIndividualSalaryPostingList(int Month, int PayrollGrp, Filter filter);
        public Task<DataSet> GetIndividualSalaryPostingList_Excel(int Month, int PayrollGrp, Filter filter);
        public Task<NetPaySplitUp> GetIndividualPostingSplitUpDetails(Guid EmployeeGI, int SalaryPeriod, int PayrollGroup);
        public Task<ResponseEntity> IndividualSalaryPosting(int SalaryPeriodCode, int EmployeeCode);

    }
}
