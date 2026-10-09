using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface ISkillMatrixService
    {
        public Task<SkillMatrix> GetSkillMatrix( Filter filterdata);
        public Task<DataSet> GetSkillMatrixReport(Filter filterdata, int type,string skills);
        public Task<DataSet> GetSkillMatrixHeader(Filter filterdata,int type,string skills);
        public Task<DataSet> GetSkillMatrixEmployeeCount(Filter filterdata, int type, string mode);
        public Task<SkillMatrix> GetSkillMatrixEmployeeDetails( Filter filterdata, int skill, string rating);
        public Task<DataSet> GetSkillNames();
        public Task<SkillMatrix> GetSkills();
    }
}
