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
  public  class PhotoGalleryService: IPhotoGalleryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PhotoGalleryService(IServiceBase httpService)
        {
            _baseURL = "PhotoGallery";
            _httpService = httpService;
        }
        public async Task<ResponseEntity> Save(PhotoGallery model)
        {
            return await _httpService.PostAsync<ResponseEntity, PhotoGallery>($"{_baseURL}/Save", model);
        }
        public async Task<PhotoGallery> PhotoGalleryList()
        {
            return await _httpService.GetAsync<PhotoGallery>($"{_baseURL}/PhotoGalleryList");
        }
        public async Task<ResponseEntity>  Delete(Guid PhotoGalleryGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/Delete?PhotoGalleryGI={PhotoGalleryGI}");
        }
        public async Task<PhotoGallery> PhotoGalleryDetails(Guid PhotoGalleryGI)
        {
            return await _httpService.GetAsync<PhotoGallery>($"{_baseURL}/PhotoGalleryDetails?PhotoGalleryGI={PhotoGalleryGI}");
        }
        public async Task<ResponseEntity> Modify(PhotoGallery model)
        {
            return await _httpService.PostAsync<ResponseEntity, PhotoGallery>($"{_baseURL}/Modify", model);
        }
    }
}
