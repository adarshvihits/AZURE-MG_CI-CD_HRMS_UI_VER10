using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public interface IHelpers
    {
        Task<string> GetFilterValues(Filter Filter);
        Task<string> GetFilterValuesMultiSelect(FilterMultiSelect Filter);
        Task<byte[]> CompressFile(IFormFile file);
        Task<byte[]> DecompressFile(byte[] compressedData);
        Task<byte[]> DecompressIfNeeded(byte[] compressedData);
        Task<string> GetCurrentDate();
    }
}
