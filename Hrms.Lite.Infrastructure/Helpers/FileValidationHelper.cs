using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public class FileSignature
    {
        public byte[] Bytes { get; set; }
        public int Offset { get; set; }
    }
    public static class FileValidationHelper
    {
        private static readonly Dictionary<string, string> _allowedProfilePicExtensions =
            new Dictionary<string, string>
            {
            { ".jpg",  "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png",  "image/png" },
            };
        private static readonly Dictionary<string, string> _allowedFileExtensions =
            new Dictionary<string, string>
            {
            { ".jpg",  "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png",  "image/png" },
            { ".pdf",  "application/pdf" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".doc",  "application/msword" },
             { ".mp4",  "video/mp4" }
            };

        private static readonly Dictionary<string, List<byte[]>> _fileSignatures =
            new Dictionary<string, List<byte[]>>
        {
                { ".jpg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF }
                    }
                },
                { ".png", new List<byte[]>
                    {
                        new byte[] { 0x89, 0x50, 0x4E, 0x47 }
                    }
                },
                { ".pdf", new List<byte[]>
                    {
                        new byte[] { 0x25, 0x50, 0x44, 0x46 }
                    }
                },
                { ".doc", new List<byte[]>
                    {
                        new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
                    }
                },
                { ".docx", new List<byte[]>
                    {
                        new byte[] { 0x50, 0x4B, 0x03, 0x04 } // ZIP signature
                    }
                }
        };

        private static readonly Dictionary<string, List<FileSignature>> _fileSignatures_ = new Dictionary<string, List<FileSignature>>
    {
        { ".jpg", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes = new byte[] { 0xFF, 0xD8, 0xFF },
                    Offset = 0
                }
            }
        },
        { ".png", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes = new byte[] { 0x89, 0x50, 0x4E, 0x47 },
                    Offset = 0
                }
            }
        },
        { ".pdf", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes = new byte[] { 0x25, 0x50, 0x44, 0x46 },
                    Offset = 0
                }
            }
        },
         { ".doc", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    Offset = 0
                }
            }
        },
         { ".docx", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes =new byte[] { 0x50, 0x4B, 0x03, 0x04 }, // ZIP signature
                    Offset = 0
                }
            }
        },


        { ".mp4", new List<FileSignature>
            {
                new FileSignature
                {
                    Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70 }, // ftyp
                    Offset = 4
                }
            }
        }

    };

        public static bool ValidateProfilePic(
            IFormFile file,
            out string errorMessage,
            long maxFileSizeInBytes = 5 * 1024 * 1024)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "File is empty.";
                return false;
            }

            if (file.Length > maxFileSizeInBytes)
            {
                errorMessage = "File size exceeds the allowed limit.";
                return false;
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedProfilePicExtensions.ContainsKey(ext))
            {
                errorMessage = "Invalid file type.";
                return false;
            }

            if (!string.Equals(file.ContentType, _allowedProfilePicExtensions[ext], StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Invalid file content type.";
                return false;
            }

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var headerBytes = reader.ReadBytes(8);

                if (!_fileSignatures.ContainsKey(ext) ||
                    !_fileSignatures[ext].Any(sig => headerBytes.Take(sig.Length).SequenceEqual(sig)))
                {
                    errorMessage = "File signature mismatch.";
                    return false;
                }
            }

            return true;
        }
        public static bool ValidateFile(
            IFormFile file,
            out string errorMessage,
            long maxFileSizeInBytes = 50 * 1024 * 1024)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "File is empty.";
                return false;
            }

            if (file.Length > maxFileSizeInBytes)
            {
                errorMessage = "File size exceeds the allowed limit.";
                return false;
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedFileExtensions.ContainsKey(ext))
            {
                errorMessage = "Invalid file type.";
                return false;
            }

            if (!string.Equals(file.ContentType, _allowedFileExtensions[ext], StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Invalid file content type.";
                return false;
            }
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var headerBytes = reader.ReadBytes(16);

                if (!_fileSignatures_.ContainsKey(ext) ||
                    !_fileSignatures_[ext].Any(sig =>
                        headerBytes.Length >= sig.Offset + sig.Bytes.Length &&
                        headerBytes
                            .Skip(sig.Offset)
                            .Take(sig.Bytes.Length)
                            .SequenceEqual(sig.Bytes)))
                {
                    errorMessage = "File signature mismatch.";
                    return false;
                }
            }


            return true;
        }
        //public static bool ValidateFile(
        //    IFormFile file,
        //    out string errorMessage,
        //    long maxFileSizeInBytes = 5 * 1024 * 1024)
        //{
        //    errorMessage = string.Empty;

        //    if (file == null || file.Length == 0)
        //    {
        //        errorMessage = "File is empty.";
        //        return false;
        //    }

        //    if (file.Length > maxFileSizeInBytes)
        //    {
        //        errorMessage = "File size exceeds the allowed limit.";
        //        return false;
        //    }

        //    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        //    if (!_allowedFileExtensions.ContainsKey(ext))
        //    {
        //        errorMessage = "Invalid file type.";
        //        return false;
        //    }

        //    if (!string.Equals(file.ContentType, _allowedFileExtensions[ext], StringComparison.OrdinalIgnoreCase))
        //    {
        //        errorMessage = "Invalid file content type.";
        //        return false;
        //    }

        //    using (var reader = new BinaryReader(file.OpenReadStream()))
        //    {
        //        var headerBytes = reader.ReadBytes(8);

        //        if (!_fileSignatures.ContainsKey(ext) ||
        //            !_fileSignatures[ext].Any(sig => headerBytes.Take(sig.Length).SequenceEqual(sig)))
        //        {
        //            errorMessage = "File signature mismatch.";
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
