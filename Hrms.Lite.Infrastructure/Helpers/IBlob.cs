using Hrms.Lite.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public interface IBlob
    {
       
        Task<File> UploadToAzureAsync(File file, string path);
        Task<File> UploadToAzureAsyncWithByteArray(File file);
        public Task<File> UploadToAzureThumbnailAsyncWithByteArray(File file);
        Task<File> UploadThumbailToAzure(File file);
        Task<bool> Delete(string fileName);
        Task<File> GetBlobFile(File file);
        public byte[] ResizeImage(int width, int height, Shared.File file);
        byte[] ResizeByteImage(int width, int height, byte[] fdata);
    }
}
