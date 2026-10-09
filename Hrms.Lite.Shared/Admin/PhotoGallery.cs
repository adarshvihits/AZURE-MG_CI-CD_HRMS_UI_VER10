using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
    public class PhotoGallery
    {
        public Guid PhotoGalleryGI { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
        public int PhotoCount { get; set; }
        public File file { get; set; }
        public List<PhotoGallery> GalleryList { get; set; }
        public List<PhotoGalleryList> PhotoGalleryList { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<File> Files1 { get; set; }
        public string FileNames { get; set; }  // This will hold the names of the selected files
        public List<string> Descriptions { get; set; } // For the image descriptions
    }
    public  class PhotoGalleryList
    {
        public File file { get; set; }
        public File ThumbnailUri { get; set; }        
        public string Description { get; set; }
    }
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public byte[] ThumbnailData { get; set; }
    }
}
