using ETicaretAPI_V2.Application.Operations;
using ETicaretAPI_V2.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI_V2.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(
                 path,
                 FileMode.Create,
                 FileAccess.Write,
                 FileShare.None,
                 1024 * 1024,
                 useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //recursive method (first?)
        //path:C:...ETicaretAPI_V2.API\wwwroot\resource\product-images\
        //filename: exampleFilename.img 
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

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            //path : kaydedilecek yer = C:...ETicaretAPI_V2.API\wwwroot\resource\product-images\
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //C:...ETicaretAPI_V2.API\wwwroot\resource\product-images\ bu pathe  dosyalarda yok ise oluşturacak!
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                //yüklenen her dosyanın  (upload path (upload neden alındı ?) ve filename) rename işlemine tabi tutulup yeni name alındı
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            return null;
        }
    }
}
