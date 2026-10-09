using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
//using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
//using Microsoft.Azure.Storage.Blob;
//using Microsoft.Azure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using SkiaSharp;
//using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Infrastructure.Helpers
{
    [SessionAuthorize]
    public class Blob : IBlob
    {
        private readonly Shared.General.Storage _storageConfig;
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region constructor
        public Blob(IOptions<AppSettings> settings, IHttpContextAccessor httpContextAccessor)
        {
            _storageConfig = settings.Value.Storage;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConfig.ConnectionString);
            _cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        //public byte[] ResizeByteImage(int width, int height, byte[] fdata)
        //{
        //    try
        //    {
        //        var resourceImage = new MemoryStream(fdata);
        //        var image = Image.FromStream(resourceImage);
        //        var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
        //        using var imageStream = new MemoryStream();
        //        thumb.Save(imageStream, ImageFormat.Jpeg);
        //        var imageBytes = imageStream.ToArray();
        //        return imageBytes;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //public byte[] ResizeImage(int width, int height, Shared.File file)
        //{
        //    try
        //    {
        //        var resourceImage = new MemoryStream(file.fdata);
        //        var image = Image.FromStream(resourceImage);
        //        var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
        //        using var imageStream = new MemoryStream();
        //        thumb.Save(imageStream, ImageFormat.Jpeg);
        //        var imageBytes = imageStream.ToArray();
        //        return imageBytes;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}






        public byte[] ResizeByteImage(int width, int height, byte[] fdata)
        {
            try
            {
                using var inputStream = new MemoryStream(fdata);
                using var original = SKBitmap.Decode(inputStream);

                if (original == null)
                    return null;

                using var resized = original.Resize(
                    new SKImageInfo(width, height),
                    SKSamplingOptions.Default);

                if (resized == null)
                    return null;

                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 90);

                return data.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public byte[] ResizeImage(int width, int height, Shared.File file)
        {
            try
            {
                if (file?.fdata == null || file.fdata.Length == 0)
                    return null;

                using var inputStream = new MemoryStream(file.fdata);
                using var original = SKBitmap.Decode(inputStream);

                if (original == null)
                    return null;

                using var resized = original.Resize(
                    new SKImageInfo(width, height),
                    SKSamplingOptions.Default);

                if (resized == null)
                    return null;

                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 90);

                return data.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #region  thumbnail to Azure (ByteArray)
        public async Task<Shared.File> UploadThumbailToAzure(Shared.File file)
        {  
            Byte[] imageBytes = ResizeImage(100, 100, file);


            Stream stream = new MemoryStream(imageBytes);
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_storageConfig.BlobContainer);
            var fileObj = GenerateFileName(file,"Thumbnail");
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new
                    BlobContainerPermissions
                {
                    PublicAccess =
                    BlobContainerPublicAccessType.Blob
                });
            }
            var cloudBlockBlob =
                cloudBlobContainer.GetBlockBlobReference(fileObj.FileName);
            cloudBlockBlob.Properties.ContentType = fileObj.FileData.ContentType; ;

            await
                cloudBlockBlob.UploadFromStreamAsync(stream);
            fileObj.AbsoluteUri = cloudBlockBlob.Uri.AbsoluteUri;
            return fileObj;
        }
        public async Task<Shared.File> UploadToAzureThumbnailAsyncWithByteArray(Shared.File file)
        {
            var stream2 = new MemoryStream(file.fdata);
            Byte[] imageBytes = ResizeImage(100, 100, file);
            Stream stream = new MemoryStream(imageBytes);
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_storageConfig.BlobContainer);
            var fileObj = GenerateFileName(file, "Thumbnail");
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new
                    BlobContainerPermissions
                {
                    PublicAccess =
                    BlobContainerPublicAccessType.Blob
                });
            }
            var cloudBlockBlob =
                cloudBlobContainer.GetBlockBlobReference(fileObj.FileName);
            cloudBlockBlob.Properties.ContentType = fileObj.ContentType;

            await
                cloudBlockBlob.UploadFromStreamAsync(stream);
            fileObj.AbsoluteUri = cloudBlockBlob.Uri.AbsoluteUri;
            return fileObj;
        }
        #endregion

        #region Image to Azure
        public async Task<Shared.File> UploadToAzureAsyncWithByteArray(Shared.File file)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_storageConfig.BlobContainer);
            var fileObj = GenerateFileName(file, "Common");
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new
                    BlobContainerPermissions
                {
                    PublicAccess =
                    BlobContainerPublicAccessType.Blob
                });
            }
            var cloudBlockBlob =
                cloudBlobContainer.GetBlockBlobReference(fileObj.FileName);
            cloudBlockBlob.Properties.ContentType = fileObj.ContentType;

            await
                cloudBlockBlob.UploadFromStreamAsync(new MemoryStream(fileObj.fdata));
            fileObj.AbsoluteUri = cloudBlockBlob.Uri.AbsoluteUri;
            return fileObj;
        }
        private Shared.File GenerateFileName_Blob(Shared.File file,string PhysicalPath)
        {
            var LoginUser = _session.GetString("User");
            Login user = new Login();
            user = JsonConvert.DeserializeObject<Login>(LoginUser);
            Guid ComapnyGuid = user.authUser.User.CompanyGuid;
            if (ComapnyGuid != Guid.Empty)
                file.RelativeUri = ComapnyGuid.ToString() + PhysicalPath;
            else
                file.RelativeUri = Guid.NewGuid().ToString() + PhysicalPath;
            var fileExtension = "";
            var fileName = "";
            if (file.FileData == null)
            {
                fileName = file.FileName;
                fileExtension = file.FileExtension;
            }
            else
            {
                fileName = Path.GetFileName(file.FileData.FileName);
                fileExtension = Path.GetExtension(file.FileData.FileName);
            }

            file.ActualFileName = fileName;
            file.FileExtension = fileExtension;
            file.FileName = $"{file.RelativeUri}{Guid.NewGuid().ToString()}{fileExtension}";
            return file;
        }

        public async Task<Shared.File> UploadToAzureAsync(Shared.File file, string PhysicalPath)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_storageConfig.BlobContainer);
            var fileObj = GenerateFileName_Blob(file, PhysicalPath);
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new
                    BlobContainerPermissions
                {
                    PublicAccess =
                    BlobContainerPublicAccessType.Blob
                });
            }
            var cloudBlockBlob =
                cloudBlobContainer.GetBlockBlobReference(fileObj.FileName);
            cloudBlockBlob.Properties.ContentType = fileObj.FileData.ContentType; ;

            await
                cloudBlockBlob.UploadFromStreamAsync(fileObj.FileData.OpenReadStream());
            fileObj.AbsoluteUri = cloudBlockBlob.Uri.AbsoluteUri;
            return fileObj;
        }
        #endregion
        #region GenerateFileName
        private Shared.File GenerateFileName(Shared.File file, string Type)
        {
            var LoginUser = _session.GetString("User");
            Login user = new Login();
            user = JsonConvert.DeserializeObject<Login>(LoginUser);
            Guid ComapnyGuid = user.authUser.User.CompanyGuid;
            file.RelativeUri = ComapnyGuid+"/employee/image";
            var fileExtension = "";
            var fileName = "";
            if (file.FileData == null)
            {
                fileName = file.FileName;
                fileExtension = file.FileExtension;
            }
            else
            {
                fileName = Path.GetFileName(file.FileData.FileName);
                fileExtension = Path.GetExtension(file.FileData.FileName);
            }   
            if (fileExtension != ".pdf")
            {
                if(Type=="Thumbnail")
                    file.RelativeUri = ComapnyGuid + "/employee/thumbnail";
                else
                file.RelativeUri = ComapnyGuid + "/employee/image";
            }
            else
                file.RelativeUri = ComapnyGuid + "/employee/PDF";
            file.ActualFileName = fileName;
            file.FileExtension = fileExtension;
            file.FileName = $"{file.RelativeUri}/{Guid.NewGuid().ToString()}{fileExtension}";
            return file;
        }
        #endregion       


        #region delete fromblob
        public async Task<bool> Delete(string fileName)
        {
            try
            {
                CloudBlobContainer cloudBlobContainer = _cloudBlobClient.GetContainerReference(_storageConfig.BlobContainer);
                if (await cloudBlobContainer.ExistsAsync())
                {
                    CloudBlob fileToDelete = cloudBlobContainer.GetBlobReference(fileName);
                    await fileToDelete.DeleteAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                var error = new LogDetails();
                error.Exception = ex.ToString();
                error.Message = $"blob delete error for image {fileName} from container {_storageConfig.BlobContainer}";
                return false;
            }
        }
        #endregion
        public async Task<Shared.File> GetBlobFile(Shared.File file)
        {
            try
            {

                var container = new BlobContainerClient(_storageConfig.ConnectionString, _storageConfig.BlobContainer);
                BlobClient blobClient = container.GetBlobClient(file.FileName);
                var transferOptions = new StorageTransferOptions
                {
                    // Set the maximum number of parallel transfer workers
                    MaximumConcurrency = 2,

                    // Set the initial transfer length to 8 MiB
                    InitialTransferSize = 8 * 1024 * 1024,

                    // Set the maximum length of a transfer to 4 MiB
                    MaximumTransferSize = 4 * 1024 * 1024
                };

                BlobDownloadToOptions downloadOptions = new BlobDownloadToOptions()
                {
                    TransferOptions = transferOptions
                };

                var stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream, downloadOptions);
                var props = await blobClient.GetPropertiesAsync().ConfigureAwait(false);
                stream.Position = 0;
                file.stream = stream;
                file.ContentType = props.Value.ContentType;
                return file;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
