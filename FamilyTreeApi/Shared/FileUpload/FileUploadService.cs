using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using FamilyTreeApi.Shared.Model;
using Shared.Services.Interface;

namespace Shared.Services.Implementation
{

    public class FileUploadService : IFileUploadService
    {
        private readonly IGenericRepository _genericRepository;
        public FileUploadService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<string> UploadFileAsync(byte[] file, string FileName, string? FolderName)
        {
            var CompanyName = await GetCompanyDetails();
            string ReturnPath;
            if (file != null)
            {
                var streamData = new MemoryStream(file);
                var UploadFolder = Path.Combine(GlobalVariable.DataDrive, CompanyName, FolderName!);
                 //FileName = Guid.NewGuid().ToString() + "_";
                var FullPath = Path.Combine(UploadFolder, FileName);
                if (!Directory.Exists(UploadFolder))
                {
                    Directory.CreateDirectory(UploadFolder);
                }
                using (var fileStream = new FileStream(FullPath, FileMode.Create))
                {
                    streamData.CopyTo(fileStream);
                }
                ReturnPath = Path.Combine(CompanyName,FolderName!, FileName);
                return ReturnPath;
            }
            else
            {
                return string.Empty;
            }
        }
        public async Task<string> GetCompanyDetails()
        {
            Dictionary<string, object> Parameter = new() { { "Flag", "F" } };
            
            var Data = await _genericRepository.GetAsync<string>("[dbo].[SpFamilyTreeSetup]", Parameter);
            return Data;
        }
    }
}
