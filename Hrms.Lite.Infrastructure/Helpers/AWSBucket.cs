using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public class AWSBucket: IAWSBucket
    {
        private readonly AwsStorage _storageConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AWSBucket(IOptions<AppSettings> settings, IHttpContextAccessor httpContextAccessor)
        {
            _storageConfig = settings.Value.AwsStorage;
            _httpContextAccessor = httpContextAccessor;
        }
       public async Task<Shared.File> UploadAwsFile(Shared.File file)
        {
            var accesskey = _storageConfig.AwsAccessKey;//provide your details
            var secretkey = _storageConfig.AwsSecretAccessKey;
            var region= _storageConfig.Region;
            //var bucketRegion = RegionEndpoint.USEast2;
            var s3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };
            var bucketName = _storageConfig.BucketName;
            var stream2 = new MemoryStream(file.fdata);
            var fileObj = GenerateFileName(file, "Common");
            var url = "https://";
            var LoginUser = _session.GetString("User");
            Login user = new Login();
            user = JsonConvert.DeserializeObject<Login>(LoginUser);
            //which we got these details , after user created in IAM Dashboard
            var s3Client = new AmazonS3Client(accesskey, secretkey, s3Config.RegionEndpoint);
            //AWSCredentials credentials = new BasicAWSCredentials(accesskey, secretkey);
            var fileTransferUtility = new TransferUtility(s3Client);//create an object for TransferUtility

            //var s3Client = new AmazonS3Client(credentials, bucketRegion);
            //string filePath = "F:\\audio.wav";//File location path
            ListBucketsResponse response = await s3Client.ListBucketsAsync();
            bool bucketExists = response.Buckets.Any(b => b.BucketName == bucketName);
            //FileStream fs =file.fdata;
            if (response.Buckets.Count == 0)
            {
                string NewbucketName = "vvnivbucket";

                // Create the bucket
                try
                {
                    await s3Client.PutBucketAsync(new PutBucketRequest
                    {
                        BucketName = NewbucketName,
                        UseClientRegion = true, // Set this to true to create the bucket in the specified region
                    });
                    
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        Console.WriteLine("Check the AWS Credentials.");
                    }
                    else
                    {
                        Console.WriteLine("Error occurred: " + amazonS3Exception.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    //creating object for    TransferUtilityUploadRequest
                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        //FilePath = filePath,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        PartSize = 6291456, // 6 MB.  
                        Key = fileObj.FileName,//filename which u want to save in bucket
                        CannedACL = S3CannedACL.PublicRead,
                        InputStream = stream2,
                    };
                    fileTransferUtility.Upload(fileTransferUtilityRequest);

                    //To upload without asynchronous
                    //fileTransferUtility.Upload(stream2, bucketName, accesskey);
                    //fileTransferUtility.Dispose();


                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        Console.WriteLine("Check the AWS Credentials.");
                    }
                    else
                    {
                        Console.WriteLine("Error occurred: " + amazonS3Exception.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
            }
            
            fileObj.AbsoluteUri = url + bucketName + ".s3." + s3Config.RegionEndpoint.SystemName + "." + s3Config.RegionEndpoint.PartitionDnsSuffix + "/" + fileObj.FileName;
            return fileObj;
        }

        public async Task<Shared.File> UploadToAWSThumbnailAsyncWithByteArray(Shared.File file)
        {
            var accesskey = _storageConfig.AwsAccessKey;//provide your details
            var secretkey = _storageConfig.AwsSecretAccessKey;
            var region = _storageConfig.Region;
            var stream2 = new MemoryStream(file.fdata);
            System.Drawing.Image resized = resizeImage(100, 100, stream2);
            using var imageStream = new MemoryStream();
            resized.Save(imageStream, ImageFormat.Jpeg);
            var imageBytes = imageStream.ToArray();
            Stream stream = new MemoryStream(imageBytes);
            //var bucketRegion = RegionEndpoint.USEast2;
            var s3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };
            var bucketName = _storageConfig.BucketName;
            var fileObj = GenerateFileName(file, "Thumbnail");
            var url = "https://";
            var LoginUser = _session.GetString("User");
            Login user = new Login();
            user = JsonConvert.DeserializeObject<Login>(LoginUser);
            //which we got these details , after user created in IAM Dashboard
            var s3Client = new AmazonS3Client(accesskey, secretkey, s3Config.RegionEndpoint);
            //AWSCredentials credentials = new BasicAWSCredentials(accesskey, secretkey);
            var fileTransferUtility = new TransferUtility(s3Client);//create an object for TransferUtility

            //var s3Client = new AmazonS3Client(credentials, bucketRegion);
            //string filePath = "F:\\audio.wav";//File location path
            try
            {
                //creating object for    TransferUtilityUploadRequest
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    //FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.  
                    Key = fileObj.FileName,//filename which u want to save in bucket
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = stream,
                };
                fileTransferUtility.Upload(fileTransferUtilityRequest);

                //To upload without asynchronous
                //fileTransferUtility.Upload(stream2, bucketName, accesskey);
                //fileTransferUtility.Dispose();


            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the AWS Credentials.");
                }
                else
                {
                    Console.WriteLine("Error occurred: " + amazonS3Exception.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
            fileObj.AbsoluteUri = url + bucketName + ".s3." + s3Config.RegionEndpoint.SystemName + "." + s3Config.RegionEndpoint.PartitionDnsSuffix + "/" + fileObj.FileName;
            return fileObj;
        }
        public Shared.File GenerateFileName(Shared.File file, string Type)
        {
            var LoginUser = _session.GetString("User");
            Login user = new Login();
            user = JsonConvert.DeserializeObject<Login>(LoginUser);
            Guid ComapnyGuid = user.authUser.User.CompanyGuid;
            file.RelativeUri = ComapnyGuid + "/employee/image";
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
            if (fileExtension != "pdf")
            {
                if (Type == "Thumbnail")
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
        public Image resizeImage(int width, int height, Stream resourceImage)
        {
            try
            {
                var image = Image.FromStream(resourceImage);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
