using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IEmployeeCertificationService
    {
        public Task<EmployeeCertification> GetList(int CertificateTypecode,int TabIndex,Filter filterdata);
        public Task<DataSet> GetList_Excel(int CertificateTypecode,int TabIndex, Filter Filter);
        
        public Task<DataSet> GetList_Summary(int CertificateTypecode, Filter Filter);
    }
}

