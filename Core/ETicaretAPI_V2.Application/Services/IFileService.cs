
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI_V2.Application.Services;//  new update! :)

public interface IFileService
{
    Task<List<(string fileName,string path)>> UploadAsync(string path , IFormFileCollection files);
    Task<string> FileRenameAsync(string fileName);
    Task<bool> CopyFileAsync(string path,IFormFile file);

}

