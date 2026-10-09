using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Admin
{
    public class DocumentCategoryService: IDocumentCategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DocumentCategoryService(IServiceBase httpService)
        {
            _baseURL = "DocumentCategory";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<DocumentCategory> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<DocumentCategory>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(DocumentCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, DocumentCategory>($"{_baseURL}/Create", input);
        }
        public Task<DocumentCategory> GetDocCategoryDetails(Guid DocCategoryGI, string type)
        {
            return _httpService.GetAsync<DocumentCategory>($"{_baseURL}/GetDocumentCategoryDetails?DocCategoryGI={DocCategoryGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(DocumentCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, DocumentCategory>($"{_baseURL}/Delete", model);
        }
        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<DocumentCategory> GetParameterList()
        {
            return await _httpService.GetAsync<DocumentCategory>($"{_baseURL}/GetParameterList");
        }
    }
}
