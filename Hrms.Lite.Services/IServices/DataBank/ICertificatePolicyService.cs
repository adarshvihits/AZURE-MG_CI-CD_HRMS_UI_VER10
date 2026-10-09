using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ICertificatePolicyService
    {
        public Task<CertificatePolicy> GetList();
        public Task<ResponseEntity> Create(CertificatePolicy Model);
        public Task<CertificatePolicy> GetDetails(int CertificateCode, string Type);

        public Task<ResponseEntity> Delete(int CertificateCode);
        public Task<ResponseEntity> CreateCertificate(CertificatePolicy Model);
        Task<File> DownloadDoc();
        public Task<CertificatePolicy> GetPopUpDetails();
    }
}
