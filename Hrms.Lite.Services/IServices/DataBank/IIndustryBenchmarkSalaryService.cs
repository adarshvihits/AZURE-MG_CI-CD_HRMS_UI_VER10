using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IIndustryBenchmarkSalaryService
    {
        public Task<IndustryBenchmarkSalary> GetIndustryBenchmarkSalaryList(string Tab,Filter filter);
        public Task<IndustryBenchmarkSalary> GetBenchmarkSalaryDetails();
        public Task<ResponseEntity> BenchmarkSalarySave(IndustryBenchmarkSalary model);
        Task<DataSet> GetIndustryBenchmarkSalaryList_Excel(string Type, Filter filter);

    }
}
