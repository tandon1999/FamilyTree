using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Interface
{
    public interface IFileUploadService
    {
         Task<string> UploadFileAsync(byte[] file,string FileName,string? FolderName);
    }
}
