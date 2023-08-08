using ETicaretAPI_V2.Application.Operations;
using Microsoft.AspNetCore.Hosting;

namespace ETicaretAPI_V2.Infrastructure.Services
{
    public class FileService 
    {
        readonly IWebHostEnvironment _webHostEnvironment;

      
        
        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string extension = Path.GetExtension(fileName);
            string newFileName;

            if (first)
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                newFileName = fileName;
                int indexNo1 = newFileName.IndexOf("-");
                if (indexNo1 == -1)
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                }
                else
                {
                    int indexNo2 = newFileName.LastIndexOf("-");
                    if (indexNo1 != indexNo2)
                    {
                        string fileNo = newFileName.Substring(indexNo2 + 1, newFileName.Length - indexNo2 - extension.Length - 1);
                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo2 + 1, newFileName.Length - indexNo2 - extension.Length - 1)
                                                     .Insert(indexNo2 + 1, _fileNo.ToString());
                        }
                        else
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                    }
                    else
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }
            }

            if (File.Exists(Path.Combine(path, newFileName)))
            {
                return await FileRenameAsync(path, newFileName, false);
            }
            else
            {
                return newFileName;
            }
        }
    }
}
