using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface ICompanyProfileService
    {
        public Task<CompanyProfile> GetCompanyProfile(Filter filter, int GroupCompanyCode);
        public Task<CompanyProfile> GetCompanyProfileDetails(int CODE,string TYPE,Filter filter,int GroupCompanyCode);
       public Task<DataSet> GetCompanyProfileEmployee_Excel(  int CODE, string TYPE, Filter filter);

    }
}
