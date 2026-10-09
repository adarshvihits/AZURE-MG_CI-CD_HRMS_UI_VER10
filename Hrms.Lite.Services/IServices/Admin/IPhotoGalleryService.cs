using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IPhotoGalleryService
    {
        Task<PhotoGallery> PhotoGalleryList();
        Task<ResponseEntity> Save(PhotoGallery model); 
        Task<ResponseEntity> Modify(PhotoGallery model);
        Task<ResponseEntity> Delete(Guid PhotoGalleryGI);
        Task<PhotoGallery> PhotoGalleryDetails(Guid PhotoGalleryGI);
    }
}
