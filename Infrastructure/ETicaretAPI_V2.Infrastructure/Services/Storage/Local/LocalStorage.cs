using ETicaretAPI_V2.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI_V2.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        => File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory= new DirectoryInfo(path);
            return directory.GetFiles().Select(p=>p.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

        async Task<bool> CopyFileAsync(string path, IFormFile file)
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
        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(IFormFileCollection files, string path)
        {
            //path : kaydedilecek yer = C:...ETicaretAPI_V2.API\wwwroot\resource\product-images\
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //C:...ETicaretAPI_V2.API\wwwroot\resource\product-images\ bu pathe  dosyalarda yok ise oluşturacak!
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                   
                await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{path}\\{file.Name}"));
                
            }
            return datas;
        }
    }
}
