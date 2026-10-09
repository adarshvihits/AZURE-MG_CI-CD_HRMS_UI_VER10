using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public interface IAWSBucket
    {
        public Task<Shared.File> UploadAwsFile(File file);
        public Task<Shared.File> UploadToAWSThumbnailAsyncWithByteArray(Shared.File file);
        public Shared.File GenerateFileName(Shared.File file, string Type);
    }
}
