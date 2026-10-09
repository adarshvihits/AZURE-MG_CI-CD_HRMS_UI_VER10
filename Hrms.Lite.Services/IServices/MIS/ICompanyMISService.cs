using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.MIS
{
    public interface ICompanyMISService
    {
        Task<CompanyMIS> GetList(int report);
        public Task<CompanyMIS> GetPayRollGP(string Diagram);
    }
}
