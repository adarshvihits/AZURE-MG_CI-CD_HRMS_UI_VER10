using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Admin
{
    public class DocumentsFolderService: IDocumentsFolderService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DocumentsFolderService(IServiceBase httpService)
        {
            _baseURL = "DocumentsFolder";
            _httpService = httpService;
        }
        public async Task<DocumentsFolder> GetList(int DocCategoryCode)
        {
            return await _httpService.GetAsync<DocumentsFolder>($"{_baseURL}/List?DocCategoryCode={DocCategoryCode}");
        }
        public Task<DocumentsFolder> GetDocumentDetails(int DocCategoryCode,Guid DocGI)
        {
            return _httpService.GetAsync<DocumentsFolder>($"{_baseURL}/GetDocumentDetails?DocCategoryCode={DocCategoryCode}&DocGI={DocGI}");
        }
        public async Task<ResponseEntity> DocumentSave(DocumentsFolder model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentsFolder>($"{_baseURL}/DocumentSave", model);
        }
        public async Task<ResponseEntity> Delete(int DocCategoryCode, Guid DocGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?DocCategoryCode={DocCategoryCode}&DocGI={DocGI}");
        }
    }
}
